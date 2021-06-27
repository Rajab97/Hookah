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
    public class FaqQuestionServiceFacade : IFaqQuestionServiceFacade
    {
        private readonly IMapper _mapper;
        private readonly IBaseService<FaqQuestion> _service;
        private readonly IUnitOfWork _unitOfWork;

        public FaqQuestionServiceFacade(IMapper mapper, IBaseService<FaqQuestion> service, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public Result<IQueryable<FaqQuestion>> GetData()
        {
            try
            {
                var data = _service.GetAll();
                return data;
            }
            catch (ApplicationException ex)
            {

                return Result<IQueryable<FaqQuestion>>.Failure(ex);
            }
            catch (Exception ex)
            {

                return Result<IQueryable<FaqQuestion>>.Failure(ExceptionMessages.FatalError);
            }
        }

        public async Task<Result<FaqQuestionViewModel>> GetModelAsync(Guid id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (!result.IsSucceed)
                {
                    return Result<FaqQuestionViewModel>.Failure(result.ExceptionMessage);
                }
                var model = _mapper.Map<FaqQuestionViewModel>(result.Data);
                return Result<FaqQuestionViewModel>.Succeed(model);
            }
            catch (ApplicationException ex)
            {

                return Result<FaqQuestionViewModel>.Failure(ex);
            }
            catch (Exception)
            {

                return Result<FaqQuestionViewModel>.Failure(ExceptionMessages.FatalError);
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

        public async Task<Result> SaveAsync(FaqQuestionViewModel model)
        {
            
            try
            {
                var isEdit = model.Id != Guid.Empty;
                FaqQuestion entity = null;

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
                    entity = _mapper.Map<FaqQuestion>(model);
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
