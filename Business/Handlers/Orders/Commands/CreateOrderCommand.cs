
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Orders.ValidationRules;
using Business.Handlers.WareHouses.Queries;
using Business.Handlers.WareHouses.Commands;

namespace Business.Handlers.Orders.Commands
{
	/// <summary>
	/// 
	/// </summary>
	public class CreateOrderCommand : IRequest<IResult>
	{
        public int CreatedUserId { get; set; }
        public int LastUpdatedUserId { get; set; }
        public bool Status { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
		public int Size { get; set; }


		public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, IResult>
		{
			private readonly IOrderRepository _orderRepository;
			private readonly IMediator _mediator;
			public CreateOrderCommandHandler(IOrderRepository orderRepository, IMediator mediator)
			{
				_orderRepository = orderRepository;
				_mediator = mediator;
			}

			[ValidationAspect(typeof(CreateOrderValidator), Priority = 1)]
			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
			{

				var result = await _mediator.Send(new ExistWareHouseByProductIdAndAmountQuery { ProductId = request.ProductId, Amount = request.Amount ,Size = request.Size });

				if (result.Data != true)
					return new ErrorResult(Messages.NameAlreadyExist);

				var getWareHouse = await _mediator.Send(new GetWareHouseByProductIdQuery { ProductId = request.ProductId, Amount = request.Amount ,Size = request.Size});

                getWareHouse.Data.Amount = getWareHouse.Data.Amount  - request.Amount;
				
				var UpdateWareHouse = await _mediator.Send(new UpdateWareHouseCommand
                {
					ProductId = getWareHouse.Data.ProductId,
					Amount = getWareHouse.Data.Amount,
					Id = getWareHouse.Data.Id,
					isReady = getWareHouse.Data.isReady,
					Status = getWareHouse.Data.Amount != 0,
					LastUpdatedUserId = getWareHouse.Data.LastUpdatedUserId,
					Size = getWareHouse.Data.Size
				});

				if (UpdateWareHouse.Success) {
                    var addedOrder = new Order
                    {
                        CreatedUserId = request.CreatedUserId,
                        LastUpdatedUserId = request.LastUpdatedUserId,
                        Status = request.Status,
                        isDeleted = false,
                        CustomerId = request.CustomerId,
                        ProductId = request.ProductId,
                        Amount = request.Amount,
						Size = request.Size,

                    };
                    _orderRepository.Add(addedOrder);
                    await _orderRepository.SaveChangesAsync();
                    return new SuccessResult(Messages.Added);
                }
				return new ErrorResult(Messages.NameAlreadyExist);
				
			}
		}
	}
}