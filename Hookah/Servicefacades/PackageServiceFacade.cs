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

        public async Task<Result> SaveAsync(PackageViewModel model)
        {
            try
            {
                using (_unitOfWork.CreateScoppedTransaction())
                {
                    var dto = _mapper.Map<Package>(model);
                   
                    var result = dto.Id == Guid.Empty? await _packageService.CreateAsync(dto) : Result.Failure();

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
