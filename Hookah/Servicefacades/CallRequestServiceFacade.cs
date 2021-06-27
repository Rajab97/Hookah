using AutoMapper;
using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using Hookah.Interfacas;
using Hookah.Models;
using Hookah.Resources;
using Hookah.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Servicefacades
{
    public class CallRequestServiceFacade : ICallRequestServiceFacade
    {
        private readonly IMapper _mapper;
        private readonly ICallRequestService _service;
        private readonly IUnitOfWork _unitOfWork;

        public CallRequestServiceFacade(IMapper mapper, ICallRequestService service, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _service = service;
            _unitOfWork = unitOfWork;
        }
        //Contact
        public async Task<Result> ContactAsync(Guid id)
        {
            try
            {
                var dataResult = await _service.GetByIdAsync(id);
                if (!dataResult.IsSucceed)
                {
                    return Result.Failure(ExceptionMessages.NotFound);
                }
                var dto = dataResult.Data;
                dto.IsContacted = true;
                var result = await _service.EditAsync(dto);
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
        public Result<IQueryable<CallRequestGridViewModel>> GetViewData()
        {
            try
            {
                var data = _service.GetViewData();
                return data;
            }
            catch (ApplicationException ex)
            {

                return Result<IQueryable<CallRequestGridViewModel>>.Failure(ex);
            }
            catch (Exception ex)
            {

                return Result<IQueryable<CallRequestGridViewModel>>.Failure(ExceptionMessages.FatalError);
            }
        }
        public Result<IQueryable<CallRequest>> GetData()
        {
            try
            {
                var data = _service.GetAll();
                return data;
            }
            catch (ApplicationException ex)
            {

                return Result<IQueryable<CallRequest>>.Failure(ex);
            }
            catch (Exception ex)
            {

                return Result<IQueryable<CallRequest>>.Failure(ExceptionMessages.FatalError);
            }
        }

        public async Task<Result<CallRequestViewModel>> GetModelAsync(Guid id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (!result.IsSucceed)
                {
                    return Result<CallRequestViewModel>.Failure(result.ExceptionMessage);
                }
                var model = _mapper.Map<CallRequestViewModel>(result.Data);
                return Result<CallRequestViewModel>.Succeed(model);
            }
            catch (ApplicationException ex)
            {

                return Result<CallRequestViewModel>.Failure(ex);
            }
            catch (Exception)
            {

                return Result<CallRequestViewModel>.Failure(ExceptionMessages.FatalError);
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

        public async Task<Result> SendCallRequestAsync(CallRequestViewModel model)
        {
            try
            {
                if (model.Day.HasValue && model.Month.HasValue && model.Year.HasValue)
                {
                    var eventDate = new DateTime(model.Year.Value, model.Month.Value, model.Day.Value);
                    if (eventDate < DateTime.Now)
                    {
                        return Result.Failure(ExceptionMessages.EventDateMustBeGreaterThanToday);
                    }
                }
                else
                {
                    return Result.Failure(ExceptionMessages.EventDateMustBeFilled);
                }
                
                var isEdit = model.Id != Guid.Empty;
                CallRequest entity = null;

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
                    entity = _mapper.Map<CallRequest>(model);
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
