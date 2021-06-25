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
    public class FooterGalaryItemServiceFacade : IFooterGalaryItemServiceFacade
    {
          private static Dictionary<string, string> fileExtentionsVsContentTypePairs = new Dictionary<string, string>() {
            { "image/jpeg",".jpeg" }
        };
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IBaseService<FooterGalaryItem> _service;
        private readonly IUnitOfWork _unitOfWork;

        public FooterGalaryItemServiceFacade( IWebHostEnvironment webHostEnvironment, IConfiguration configuration,IMapper mapper,IBaseService<FooterGalaryItem> FooterGalaryItemService,IUnitOfWork unitOfWork)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _mapper = mapper;
            _service = FooterGalaryItemService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<FooterGalaryItemViewModel>> GetModelAsync(Guid id)
        {
            try
            {
                var dtoResult = await _service.GetByIdAsync(id);
                if (!dtoResult.IsSucceed)
                    return Result<FooterGalaryItemViewModel>.Failure(dtoResult.ExceptionMessage);

                var model = _mapper.Map<FooterGalaryItemViewModel>(dtoResult.Data);
                return Result<FooterGalaryItemViewModel>.Succeed(model);
            }
            catch (ApplicationException ex)
            {

                return Result<FooterGalaryItemViewModel>.Failure(ex);
            }
            catch (Exception ex)
            {

                return Result<FooterGalaryItemViewModel>.Failure(ExceptionMessages.FatalError);
            }
            
        }
        public Result<IQueryable<FooterGalaryItem>> GetData()
        {
            try
            {
                var data = _service.GetAll();
                return data;
            }
            catch (ApplicationException ex)
            {
               
                return Result<IQueryable<FooterGalaryItem>>.Failure(ex);
            }
            catch (Exception ex)
            {
               
                return Result<IQueryable<FooterGalaryItem>>.Failure(ExceptionMessages.FatalError);
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
        public async Task<Result> SaveAsync(FooterGalaryItemViewModel model)
        {
            var isEdit = model.Id != Guid.Empty;
            var isAnyFileAdded = model.FieldName != null && model.FieldName.Any();
            string directoryPath = string.Empty;
            if (!isEdit && !isAnyFileAdded)
                return Result.Failure(ExceptionMessages.FileMustBeAdded);
            List<string> filePaths = new List<string>();

            try
            {
                FooterGalaryItem dto = null;
                if (!isEdit)
                {
                    dto = _mapper.Map<FooterGalaryItem>(model);
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
