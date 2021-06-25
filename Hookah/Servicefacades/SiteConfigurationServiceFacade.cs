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
    public class SiteConfigurationServiceFacade : ISiteConfigurationServiceFacade
    {
        private static Dictionary<string, string> fileExtentionsVsContentTypePairs = new Dictionary<string, string>() {
            { "image/jpeg",".jpeg" }
        };
        private readonly ISiteConfigurationService _service;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public SiteConfigurationServiceFacade(ISiteConfigurationService service,IMapper mapper,IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment,IConfiguration configuration)
        {
            _service = service;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }
        public async Task<Result<SiteConfigurationViewModel>> GetDefaultModelAsync()
        {
            try
            {
                var result = await _service.GetDefaultDataAsync();
                if (result.IsSucceed)
                {
                    var model = _mapper.Map<SiteConfigurationViewModel>(result.Data);
                    return Result<SiteConfigurationViewModel>.Succeed(model);
                }
                return Result<SiteConfigurationViewModel>.Succeed(new SiteConfigurationViewModel());
            }
            catch (ApplicationException ex)
            {
                return Result<SiteConfigurationViewModel>.Failure(ex.Message);
            }
            catch (Exception e)
            {
                return Result<SiteConfigurationViewModel>.Failure(e);
            }
        }

        public async Task<Result> SaveAsync(SiteConfigurationViewModel model)
        {
            var isEdit = model.Id != Guid.Empty;
            var isAnyFileAdded = model.FieldName != null && model.FieldName.Any();
            string directoryPath = string.Empty;
            if (!isEdit && !isAnyFileAdded)
                return Result.Failure(ExceptionMessages.FileMustBeAdded);
            List<string> filePaths = new List<string>();
            List<string> updatedFilePaths = new List<string>();
            try
            {
                SiteConfiguration dto = null;
                if (!isEdit)
                {
                    dto = _mapper.Map<SiteConfiguration>(model);
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

               // var previousImagePath = dto.ImagePath;
                if (isAnyFileAdded)
                {
                    for (int i = 0; i < model.FileName.Count; i++)
                    {
                        var date = DateTime.Now;
                        directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, _configuration.GetValue<string>("FileSettings:FolderName"));
                        directoryPath = Path.Combine(directoryPath, date.Year.ToString(), date.Month.ToString(), date.Day.ToString());

                        var fileExtension = Path.GetExtension(model.FileName[i]);
                        var base64String = model.Base64String[i].Substring(model.Base64String[i].IndexOf(',') + 1);
                        byte[] bytes = Convert.FromBase64String(base64String);

                        if (String.IsNullOrEmpty(fileExtension) && fileExtentionsVsContentTypePairs.TryGetValue(model.ContentType[i], out var fileEx))
                            fileExtension = fileEx;

                        string fileName = Guid.NewGuid().ToString().Replace('/', 'z');
                        var contentType = model.ContentType[i];
                        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
                        var filePath = Path.Combine(directoryPath, fileName + fileExtension);
                        await File.WriteAllBytesAsync(filePath, bytes);
                        filePaths.Add(filePath);

                        var exFilePath = dto.GetType().GetProperty(model.FieldName[i]).GetValue(dto);
                        if (exFilePath != null)
                            updatedFilePaths.Add(exFilePath.ToString());
                        
                        dto.GetType().GetProperty(model.FieldName[i]).SetValue(dto, filePath.Substring(filePath.IndexOf(_configuration.GetValue<string>("FileSettings:FolderName"))));
                    }
                }

                var result = model.Id == Guid.Empty ? await _service.CreateAsync(dto) : await _service.EditAsync(dto);

                //Remove Ex Image From Folder
                if (isEdit && updatedFilePaths.Any())
                {
                    foreach (var filePath in updatedFilePaths)
                    {
                        var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath);
                        if (File.Exists(fullPath)) File.Delete(fullPath);
                    }
                 
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
