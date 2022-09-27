
using Business.Handlers.WareHouses.Commands;
using FluentValidation;

namespace Business.Handlers.WareHouses.ValidationRules
{

	public class CreateWareHouseValidator : AbstractValidator<CreateWareHouseCommand>
	{
		public CreateWareHouseValidator()
		{
			RuleFor(x => x.Amount).NotEmpty();
			RuleFor(x => x.isReady).NotEmpty();
			RuleFor(x => x.Size).NotEmpty();

		}
	}
	public class UpdateWareHouseValidator : AbstractValidator<UpdateWareHouseCommand>
	{
		public UpdateWareHouseValidator()
		{
			RuleFor(x => x.Amount).NotEmpty();
			RuleFor(x => x.isReady).NotEmpty();
			RuleFor(x => x.Size).NotEmpty();

		}
	}
}