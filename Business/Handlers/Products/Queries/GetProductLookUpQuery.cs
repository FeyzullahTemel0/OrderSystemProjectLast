
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Entities.Dtos;
using Business.Handlers.Languages.Queries;
using Core.Entities.Dtos;
using DataAccess.Concrete.EntityFramework;
using System.Collections.Generic;

namespace Business.Handlers.Products.Queries
{

	public class GetProductLookUpQuery : IRequest<IDataResult<ProductDto>>
	{

		public class GetProductLookUpQueryHandler : IRequestHandler<GetProductLookUpQuery, IDataResult<ProductDto>>
		{
			private readonly IProductRepository _productRepository;
			private readonly IMediator _mediator;

			public GetProductLookUpQueryHandler(IProductRepository productRepository, IMediator mediator)
			{
				_productRepository = productRepository;
				_mediator = mediator;
			}
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetLanguagesLookUpWithCodeQuery request, CancellationToken cancellationToken)
			{
                return new SuccessDataResult<IEnumerable<SelectionItem>>(
                    await _productRepository.GetProductLookUpWithCode());
            }

			public Task<IDataResult<ProductDto>> Handle(GetProductLookUpQuery request, CancellationToken cancellationToken)
			{
				throw new System.NotImplementedException();
			}
		}
	}
}
