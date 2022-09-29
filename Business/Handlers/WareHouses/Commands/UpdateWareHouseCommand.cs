
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.WareHouses.ValidationRules;


namespace Business.Handlers.WareHouses.Commands
{


	public class UpdateWareHouseCommand : IRequest<IResult>
	{
        public int Id { get; set; }
        public int LastUpdatedUserId { get; set; }
        public bool Status { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public bool isReady { get; set; }
        public int Size { get; set; }

        public class UpdateWareHouseCommandHandler : IRequestHandler<UpdateWareHouseCommand, IResult>
		{
			private readonly IWareHouseRepository _wareHouseRepository;
			private readonly IMediator _mediator;

			public UpdateWareHouseCommandHandler(IWareHouseRepository wareHouseRepository, IMediator mediator)
			{
				_wareHouseRepository = wareHouseRepository;
				_mediator = mediator;
			}

			[ValidationAspect(typeof(UpdateWareHouseValidator), Priority = 1)]
			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(UpdateWareHouseCommand request, CancellationToken cancellationToken)
			{
				var isThereWareHouseRecord = await _wareHouseRepository.GetAsync(u => u.Id == request.Id);


                isThereWareHouseRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereWareHouseRecord.LastUpdatedDate = System.DateTime.Now;
                isThereWareHouseRecord.Status = request.Status;
                isThereWareHouseRecord.ProductId = request.ProductId;
                isThereWareHouseRecord.Amount = request.Amount;
                isThereWareHouseRecord.isReady = request.isReady;
                isThereWareHouseRecord.Size = request.Size;



                _wareHouseRepository.Update(isThereWareHouseRecord);
				await _wareHouseRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Updated);
			}
		}
	}
}

