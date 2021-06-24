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
    public class CatheringEventServiceFacade : ICatheringEventServiceFacade
    {
        private readonly IMapper _mapper;
        private readonly IBaseService<CatheringEvent> _service;
        private readonly IUnitOfWork _unitOfWork;

        public CatheringEventServiceFacade(IMapper mapper, IBaseService<CatheringEvent> service, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public Result<IQueryable<CatheringEvent>> GetData()
        {
            try
            {
                var data = _service.GetAll();
                return data;
            }
            catch (ApplicationException ex)
            {

                return Result<IQueryable<CatheringEvent>>.Failure(ex);
            }
            catch (Exception ex)
            {

                return Result<IQueryable<CatheringEvent>>.Failure(ExceptionMessages.FatalError);
            }
        }

        public async Task<Result<CatheringEventViewModel>> GetModelAsync(Guid id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (!result.IsSucceed)
                {
                    return Result<CatheringEventViewModel>.Failure(result.ExceptionMessage);
                }
                var model = _mapper.Map<CatheringEventViewModel>(result.Data);
                return Result<CatheringEventViewModel>.Succeed(model);
            }
            catch (ApplicationException ex)
            {

                return Result<CatheringEventViewModel>.Failure(ex);
            }
            catch (Exception)
            {

                return Result<CatheringEventViewModel>.Failure(ExceptionMessages.FatalError);
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

        public async Task<Result> SaveAsync(CatheringEventViewModel model)
        {
            
            try
            {
                var isEdit = model.Id != Guid.Empty;
                CatheringEvent entity = null;

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
                    entity = _mapper.Map<CatheringEvent>(model);
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
