
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
namespace Business.Handlers.WareHouses.Queries
{

	public class GetWareHouseByProductIdQuery : IRequest<IDataResult<WareHouse>>
	{
		public int ProductId { get; set; }
		public int Size { get; set; }
		public int Amount { get; set; }
		public	class GetWareHouseByProductIdQueryHandler : IRequestHandler<GetWareHouseByProductIdQuery, IDataResult<WareHouse>>
		{
			private readonly IWareHouseRepository _wareHouseRepository;
			private readonly IMediator _mediator;

			public GetWareHouseByProductIdQueryHandler(IWareHouseRepository wareHouseRepository,IMediator mediator)
			{
				_wareHouseRepository = wareHouseRepository;
				_mediator = mediator;
			}
			[LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
			public async Task<IDataResult<WareHouse>> Handle(GetWareHouseByProductIdQuery request, CancellationToken cancellationToken)
			{
				var WareHouseProductId = await _wareHouseRepository.GetAsync(u=>u.ProductId==request.ProductId  && u.Size==request.Size && u.isDeleted==false && u.isReady==true );

				return new SuccessDataResult<WareHouse>(WareHouseProductId);
			}
		}
	}
}
