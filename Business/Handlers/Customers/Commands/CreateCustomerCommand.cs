﻿
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
using Business.Handlers.Customers.ValidationRules;

namespace Business.Handlers.Customers.Commands
{
	/// <summary>
	/// 
	/// </summary>
	public class CreateCustomerCommand : IRequest<IResult>
	{

        public int CreatedUserId { get; set; }
        public int LastUpdatedUserId { get; set; }
        public bool Status { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }


        public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, IResult>
		{
			private readonly ICustomerRepository _customerRepository;
			private readonly IMediator _mediator;
			public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMediator mediator)
			{
				_customerRepository = customerRepository;
				_mediator = mediator;
			}

			[ValidationAspect(typeof(CreateCustomerValidator), Priority = 1)]
			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
			{
                var isThereCustomerRecord = _customerRepository.Query().Any(u=>u.CustomerName == request.CustomerName && u.isDeleted == false);
                // düzenleme yapıldı

                if (isThereCustomerRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedCustomer = new Customer
                {
                    //Düzenlemeler yapıldı // ***
                    CreatedUserId = request.CreatedUserId,
                    CreatedDate = System.DateTime.Now,
                    LastUpdatedUserId = request.LastUpdatedUserId,
                    LastUpdatedDate = System.DateTime.Now,
                    Status = request.Status,
                    isDeleted = false,
                    CustomerName = request.CustomerName,
                    CustomerCode = request.CustomerCode,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,

                };

                _customerRepository.Add(addedCustomer);
				await _customerRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Added);
			}
		}
	}
}