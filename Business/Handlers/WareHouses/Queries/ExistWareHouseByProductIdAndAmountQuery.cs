
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

	public class ExistWareHouseByProductIdAndAmountQuery : IRequest<IDataResult<bool>>
	{
		public int ProductId { get; set; }
		public int Amount { get; set; }
		public int Size { get; set; }
		public class ExistWareHouseByProductIdAndAmountQueryHandler : IRequestHandler<ExistWareHouseByProductIdAndAmountQuery, IDataResult<bool>>
		{
			private readonly IWareHouseRepository _wareHouseRepository;
			private readonly IMediator _mediator;

			public ExistWareHouseByProductIdAndAmountQueryHandler(IWareHouseRepository wareHouseRepository, IMediator mediator)
			{
				_wareHouseRepository = wareHouseRepository;
				_mediator = mediator;
			}
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IDataResult<bool>> Handle(ExistWareHouseByProductIdAndAmountQuery request, CancellationToken cancellationToken)
			{

				return new SuccessDataResult<bool>(await _wareHouseRepository.ExistWareHouseByProductIdAndAmount(request.ProductId,request.Amount,request.Size));
			}
		}
	}
}
