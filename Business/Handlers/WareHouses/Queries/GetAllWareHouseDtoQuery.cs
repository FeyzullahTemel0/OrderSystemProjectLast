
using Business.BusinessAspects;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Performance;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Caching;
using Entities.Dtos;

namespace Business.Handlers.WareHouses.Queries
{

	public class GetAllWareHouseDtoQuery : IRequest<IDataResult<IEnumerable<WareHouseDto>>>
	{
		public class GetAllWareHouseDtoQueryHandler : IRequestHandler<GetAllWareHouseDtoQuery, IDataResult<IEnumerable<WareHouseDto>>>
		{
			private readonly IWareHouseRepository _wareHouseRepository;
			private readonly IMediator _mediator;

			public GetAllWareHouseDtoQueryHandler(IWareHouseRepository wareHouseRepository, IMediator mediator)
			{
				_wareHouseRepository = wareHouseRepository;
				_mediator = mediator;
			}

			[PerformanceAspect(5)]
			[CacheAspect(10)]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IDataResult<IEnumerable<WareHouseDto>>> Handle(GetAllWareHouseDtoQuery request, CancellationToken cancellationToken)
			{
				return new SuccessDataResult<IEnumerable<WareHouseDto>>( await _wareHouseRepository.GetAllWareHouseDetailsDto());
			}
		}
	}
}