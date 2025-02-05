using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleValidator()
        {
            RuleFor(x => x.SaleNumber)
                .NotEmpty().WithMessage("Sale number is required")
                .MaximumLength(50).WithMessage("Sale number must not exceed 50 characters");

            RuleFor(x => x.SaleDate)
                .NotEmpty().WithMessage("Sale date is required");

            RuleFor(x => x.Client)
                .NotEmpty().WithMessage("Client is required")
                .MaximumLength(100).WithMessage("Client name must not exceed 100 characters");

            RuleFor(x => x.TotalValue)
                .GreaterThan(0).WithMessage("Total value must be greater than 0");

            RuleFor(x => x.Branch)
                .NotEmpty().WithMessage("Branch is required");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("At least one item is required")
                .Must(x => x.Count > 0).WithMessage("At least one item must be provided");

            RuleForEach(x => x.Items).SetValidator(new CreateSaleItemValidator());
        }
    }

    public class CreateSaleItemValidator : AbstractValidator<CreateSaleItemRequest>
    {
        public CreateSaleItemValidator()
        {
            RuleFor(x => x.Product)
                .NotEmpty().WithMessage("Product name is required");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than 0");
        }
    }
}
