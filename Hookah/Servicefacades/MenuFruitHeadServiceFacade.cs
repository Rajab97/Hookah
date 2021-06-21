using AutoMapper;
using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using Hookah.Interfacas;
using Hookah.Models;
using Hookah.Resources;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Servicefacades
{
    public class MenuFruitHeadServiceFacade : IMenuFruitHeadServiceFacade
    {
          private static Dictionary<string, string> fileExtentionsVsContentTypePairs = new Dictionary<string, string>() {
            { "image/jpeg",".jpeg" }
        };
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IMenuFruitHeadService _service;
        private readonly IUnitOfWork _unitOfWork;

        public MenuFruitHeadServiceFacade( IWebHostEnvironment webHostEnvironment, IConfiguration configuration,IMapper mapper,IMenuFruitHeadService menuFruitHeadService,IUnitOfWork unitOfWork)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _mapper = mapper;
            _service = menuFruitHeadService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<MenuFruitHeadViewModel>> GetModelAsync(Guid id)
        {
            try
            {
                var dtoResult = await _service.GetByIdAsync(id);
                if (!dtoResult.IsSucceed)
                    return Result<MenuFruitHeadViewModel>.Failure(dtoResult.ExceptionMessage);

                var model = _mapper.Map<MenuFruitHeadViewModel>(dtoResult.Data);
                return Result<MenuFruitHeadViewModel>.Succeed(model);
            }
            catch (ApplicationException ex)
            {

                return Result<MenuFruitHeadViewModel>.Failure(ex);
            }
            catch (Exception ex)
            {

                return Result<MenuFruitHeadViewModel>.Failure(ExceptionMessages.FatalError);
            }
            
        }
        public Result<IQueryable<MenuFruitHead>> GetData()
        {
            try
            {
                var data = _service.GetAll();
                return data;
            }
            catch (ApplicationException ex)
            {
               
                return Result<IQueryable<MenuFruitHead>>.Failure(ex);
            }
            catch (Exception ex)
            {
               
                return Result<IQueryable<MenuFruitHead>>.Failure(ExceptionMessages.FatalError);
            }
        }
        public async Task<Result> RemoveAsync(Guid id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                if (result.IsSucceed)
                {
                    await _unitOfWork.CommitAsync();
                }
                return result;
            }
            catch (ApplicationException ex)
            {

                return Result.Failure(ex);
            }
            catch (Exception ex)
            {

                return Result.Failure(ExceptionMessages.FatalError);
            }
          
        }
        public async Task<Result> SaveAsync(MenuFruitHeadViewModel model)
        {
            var isEdit = model.Id != Guid.Empty;
            var isAnyFileAdded = model.FieldName != null && model.FieldName.Any();
            string directoryPath = string.Empty;
            if (!isEdit && !isAnyFileAdded)
                return Result.Failure(ExceptionMessages.FileMustBeAdded);
            List<string> filePaths = new List<string>();

            try
            {
                MenuFruitHead dto = null;
                if (!isEdit)
                {
                    dto = _mapper.Map<MenuFruitHead>(model);
                }
                else
                {
                    var exData = await _service.GetByIdAsync(model.Id);
                    if (!exData.IsSucceed)
                    {
                        return Result.Failure(ExceptionMessages.NotFound);
                    }
                    dto = _mapper.Map(model, exData.Data);
                }

                var previousImagePath = dto.ImagePath;
                if (isAnyFileAdded)
                {
                    var date = DateTime.Now;
                    directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, _configuration.GetValue<string>("FileSettings:FolderName"));
                    directoryPath = Path.Combine(directoryPath, date.Year.ToString(), date.Month.ToString(), date.Day.ToString());

                    var fileExtension = Path.GetExtension(model.FileName[0]);
                    var base64String = model.Base64String[0].Substring(model.Base64String[0].IndexOf(',') + 1);
                    byte[] bytes = Convert.FromBase64String(base64String);

                    if (String.IsNullOrEmpty(fileExtension) && fileExtentionsVsContentTypePairs.TryGetValue(model.ContentType[0], out var fileEx))
                        fileExtension = fileEx;

                    string fileName = Guid.NewGuid().ToString().Replace('/', 'z');
                    var contentType = model.ContentType[0];
                    if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
                    var filePath = Path.Combine(directoryPath, fileName + fileExtension);
                    await File.WriteAllBytesAsync(filePath, bytes);
                    filePaths.Add(filePath);
                    dto.ImagePath = filePath.Substring(filePath.IndexOf(_configuration.GetValue<string>("FileSettings:FolderName")));
                }

                var result = model.Id == Guid.Empty ? await _service.CreateAsync(dto) : await _service.EditAsync(dto);

                //Remove Ex Image From Folder
                if (isEdit && isAnyFileAdded)
                {
                    previousImagePath = Path.Combine(_webHostEnvironment.WebRootPath, previousImagePath);
                    if (File.Exists(previousImagePath)) File.Delete(previousImagePath);
                }

                if (result.IsSucceed)
                {
                    await _unitOfWork.CommitAsync();
                }

              
                return Result.Succeed();
            }
            catch (ApplicationException ex)
            {
                foreach (var path in filePaths)
                {
                    if (File.Exists(path)) File.Delete(path);
                }
                return Result.Failure(ex);
            }
            catch (Exception ex)
            {
                foreach (var path in filePaths)
                {
                    if (File.Exists(path)) File.Delete(path);
                }
                return Result.Failure(ExceptionMessages.FatalError);
            }
        }
    }
}
