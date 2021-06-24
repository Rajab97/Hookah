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
    public class HowItWorksStepServiceFacade : IHowItWorksStepServiceFacade
    {
          private static Dictionary<string, string> fileExtentionsVsContentTypePairs = new Dictionary<string, string>() {
            { "image/jpeg",".jpeg" }
        };
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IBaseService<HowItWorksStep> _service;
        private readonly IUnitOfWork _unitOfWork;

        public HowItWorksStepServiceFacade( IWebHostEnvironment webHostEnvironment, IConfiguration configuration,IMapper mapper,IBaseService<HowItWorksStep> HowItWorksStepService,IUnitOfWork unitOfWork)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _mapper = mapper;
            _service = HowItWorksStepService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<HowItWorksStepViewModel>> GetModelAsync(Guid id)
        {
            try
            {
                var dtoResult = await _service.GetByIdAsync(id);
                if (!dtoResult.IsSucceed)
                    return Result<HowItWorksStepViewModel>.Failure(dtoResult.ExceptionMessage);

                var model = _mapper.Map<HowItWorksStepViewModel>(dtoResult.Data);
                return Result<HowItWorksStepViewModel>.Succeed(model);
            }
            catch (ApplicationException ex)
            {

                return Result<HowItWorksStepViewModel>.Failure(ex);
            }
            catch (Exception ex)
            {

                return Result<HowItWorksStepViewModel>.Failure(ExceptionMessages.FatalError);
            }
            
        }
        public Result<IQueryable<HowItWorksStep>> GetData()
        {
            try
            {
                var data = _service.GetAll();
                return data;
            }
            catch (ApplicationException ex)
            {
               
                return Result<IQueryable<HowItWorksStep>>.Failure(ex);
            }
            catch (Exception ex)
            {
               
                return Result<IQueryable<HowItWorksStep>>.Failure(ExceptionMessages.FatalError);
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
        public async Task<Result> SaveAsync(HowItWorksStepViewModel model)
        {
            var isEdit = model.Id != Guid.Empty;
            var isAnyFileAdded = model.FieldName != null && model.FieldName.Any();
            string directoryPath = string.Empty;
            if (!isEdit && !isAnyFileAdded)
                return Result.Failure(ExceptionMessages.FileMustBeAdded);
            List<string> filePaths = new List<string>();

            try
            {
                HowItWorksStep dto = null;
                if (!isEdit)
                {
                    dto = _mapper.Map<HowItWorksStep>(model);
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
