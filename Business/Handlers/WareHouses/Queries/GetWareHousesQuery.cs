
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.WareHouses.Queries
{

    public class GetWareHousesQuery : IRequest<IDataResult<IEnumerable<WareHouse>>>
    {
        public class GetWareHousesQueryHandler : IRequestHandler<GetWareHousesQuery, IDataResult<IEnumerable<WareHouse>>>
        {
            private readonly IWareHouseRepository _wareHouseRepository;
            private readonly IMediator _mediator;

            public GetWareHousesQueryHandler(IWareHouseRepository wareHouseRepository, IMediator mediator)
            {
                _wareHouseRepository = wareHouseRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<WareHouse>>> Handle(GetWareHousesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<WareHouse>>(await _wareHouseRepository.GetListAsync(x =>x.isDeleted==false));
            }
        }
    }
}