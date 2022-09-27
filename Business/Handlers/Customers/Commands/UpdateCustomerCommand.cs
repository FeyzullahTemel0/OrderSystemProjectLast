﻿
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
using Business.Handlers.Customers.ValidationRules;


namespace Business.Handlers.Customers.Commands
{


	public class UpdateCustomerCommand : IRequest<IResult>
	{
        // Update created userıd ye ihtiyacım yok 
        public int Id { get; set; }
        public int LastUpdatedUserId { get; set; }
        public bool Status { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, IResult>
		{
			private readonly ICustomerRepository _customerRepository;
			private readonly IMediator _mediator;

			public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMediator mediator)
			{
				_customerRepository = customerRepository;
				_mediator = mediator;
			}

			[ValidationAspect(typeof(UpdateCustomerValidator), Priority = 1)]
			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
			{
                var isThereCustomerRecord = await _customerRepository.GetAsync(u => u.Id == request.Id);

                // update yaparken bu kayıttan daha önce var mı kontrolü yapılması gerekir..

                isThereCustomerRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereCustomerRecord.LastUpdatedDate = System.DateTime.Now;
                isThereCustomerRecord.Status = request.Status;
                isThereCustomerRecord.CustomerName = request.CustomerName;
                isThereCustomerRecord.CustomerCode = request.CustomerCode;
                isThereCustomerRecord.Address = request.Address;
                isThereCustomerRecord.PhoneNumber = request.PhoneNumber;
                isThereCustomerRecord.Email = request.Email;

                // Sistemde aynı isimde customer name var ise güncelleme yapmicak
                //Controller


                _customerRepository.Update(isThereCustomerRecord);
                await _customerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
		}
	}
}

