
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.WareHouses.Queries
{
    public class GetWareHouseQuery : IRequest<IDataResult<WareHouse>>
    {
        public int Id { get; set; }

        public class GetWareHouseQueryHandler : IRequestHandler<GetWareHouseQuery, IDataResult<WareHouse>>
        {
            private readonly IWareHouseRepository _wareHouseRepository;
            private readonly IMediator _mediator;

            public GetWareHouseQueryHandler(IWareHouseRepository wareHouseRepository, IMediator mediator)
            {
                _wareHouseRepository = wareHouseRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<WareHouse>> Handle(GetWareHouseQuery request, CancellationToken cancellationToken)
            {
                var wareHouse = await _wareHouseRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<WareHouse>(wareHouse);
            }
        }
    }
}
