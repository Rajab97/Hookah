using AutoMapper;
using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using Hookah.Interfacas;
using Hookah.Models;
using Hookah.Resources;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Servicefacades
{
    public class MenuFlavorServiceFacade : IMenuFlavorServiceFacade
    {
        private readonly IMapper _mapper;
        private readonly IMenuFlavorService _service;
        private readonly IUnitOfWork _unitOfWork;

        public MenuFlavorServiceFacade(IMapper mapper, IMenuFlavorService service, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public Result<IQueryable<MenuFlavor>> GetData()
        {
            try
            {
                var data = _service.GetAll();
                return data;
            }
            catch (ApplicationException ex)
            {

                return Result<IQueryable<MenuFlavor>>.Failure(ex);
            }
            catch (Exception ex)
            {

                return Result<IQueryable<MenuFlavor>>.Failure(ExceptionMessages.FatalError);
            }
        }

        public async Task<Result<MenuFlavorViewModel>> GetModelAsync(Guid id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (!result.IsSucceed)
                {
                    return Result<MenuFlavorViewModel>.Failure(result.ExceptionMessage);
                }
                var model = _mapper.Map<MenuFlavorViewModel>(result.Data);
                return Result<MenuFlavorViewModel>.Succeed(model);
            }
            catch (ApplicationException ex)
            {

                return Result<MenuFlavorViewModel>.Failure(ex);
            }
            catch (Exception)
            {

                return Result<MenuFlavorViewModel>.Failure(ExceptionMessages.FatalError);
            }
        }

        public async Task<Result> RemoveAsync(Guid id)
        {
            try
            {
               var result = await _service.DeleteAsync(id);
                if (!result.IsSucceed)
                {
                    return Result.Failure(result.ExceptionMessage);
                }
                await _unitOfWork.CommitAsync();
                return result;
            }
            catch (ApplicationException ex)
            {

                return Result.Failure(ex);
            }
            catch (Exception)
            {

                return Result.Failure(ExceptionMessages.FatalError);
            }
        }

        public async Task<Result> SaveAsync(MenuFlavorViewModel model)
        {
            
            try
            {
                var isEdit = model.Id != Guid.Empty;
                MenuFlavor entity = null;

                if (isEdit)
                {
                    var exDataResult = await _service.GetByIdAsync(model.Id);
                    if (!exDataResult.IsSucceed)
                    {
                        return Result.Failure(ExceptionMessages.NotFound);
                    }
                    entity = _mapper.Map(model, exDataResult.Data);
                }
                else
                {
                    entity = _mapper.Map<MenuFlavor>(model);
                }

                var result = isEdit ? await _service.EditAsync(entity) : await _service.CreateAsync(entity);
               
                if (result.IsSucceed)
                {
                    await _unitOfWork.CommitAsync();
                    return Result.Succeed();
                }
                return Result.Failure(ExceptionMessages.NotFound);
            
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
    }
}
