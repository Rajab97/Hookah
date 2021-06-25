using AutoMapper;
using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using Hookah.Exceptions;
using Hookah.Interfacas;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Servicefacades
{
    public class PackageServiceFacade : IPackageServiceFacade
    {
        private readonly IPackageService _packageService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PackageServiceFacade(IPackageService packageService,IMapper mapper,IUnitOfWork unitOfWork)
        {
            this._packageService = packageService;
            this._mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Result Edit(PackageItemViewModel packageItemViewModel)
        {
            throw new NotImplementedException();
        }

        public Result<IQueryable<Package>> GetAll()
        {
            try
            {
               var result =  _packageService.GetAll();
                return result;
            }
            catch (BaseException exc)
            {
                return Result<IQueryable<Package>>.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result<IQueryable<Package>>.Failure(fatalExc);
            }
        }

        public async Task<Result<PackageViewModel>> GetModelByIdAsync(Guid packageId)
        {
            try
            {
                var dtoResult = await _packageService.GetByIdAsync(packageId);
                if (!dtoResult.IsSucceed)
                    return Result<PackageViewModel>.Failure(dtoResult.ExceptionMessage);

                var model = _mapper.Map<PackageViewModel>(dtoResult.Data);
                return Result<PackageViewModel>.Succeed(model);
            }
            catch (BaseException exc)
            {
                return Result<PackageViewModel>.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result<PackageViewModel>.Failure(fatalExc);
            }
           
        }

        public Result<IQueryable<PackageItem>> GetPackageItems(Guid packageId)
        {
            var dataResult = _packageService.GetPackageItems(packageId);
            if (dataResult.IsSucceed)
            {
                return Result<IQueryable<PackageItem>>.Succeed(dataResult.Data);
            }
            return Result<IQueryable<PackageItem>>.Failure(dataResult.ExceptionMessage);
        }
        public async Task<Result> RemoveAsync(Guid Id)
        {
            try
            {
               var result = await _packageService.DeleteAsync(Id);
                if (result.IsSucceed)
                {
                    await _unitOfWork.CommitAsync();
                }
                return result;
            }
            catch (BaseException exc)
            {
                return Result.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result.Failure(fatalExc);
            }
        }
        public async Task<Result> SaveAsync(PackageViewModel model)
        {
            try
            {
                using (_unitOfWork.CreateScoppedTransaction())
                {
                    var result = new Result();
                    if (model.Id == Guid.Empty)
                    {
                        var dto = _mapper.Map<Package>(model);
                        result = await _packageService.CreateAsync(dto);
                    }
                    else
                    {
                        var exData = await _packageService.GetByIdAsync(model.Id);
                        if (!exData.IsSucceed)
                            return Result.Failure(exData.ExceptionMessage);
                        var dto = _mapper.Map( model, exData.Data);
                        result = await _packageService.EditAsync(dto);
                    }
                 

                    if (result.IsSucceed)
                    {
                        await _unitOfWork.CommitAsync();
                        return Result.Succeed();
                    }
                    return Result.Failure(result.ExceptionMessage);
                }
            
            }
            catch (BaseException exc)
            {
                return Result.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result.Failure(fatalExc);
            }
        }
    }
}
