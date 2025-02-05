using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;
using FluentValidation;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(x => x.SaleNumber).NotEmpty().WithMessage("Sale number is required.");
        RuleFor(x => x.SaleDate).NotEmpty().WithMessage("Sale date is required.");
        RuleFor(x => x.Client).NotEmpty().WithMessage("Client is required.");
        RuleFor(x => x.TotalValue).GreaterThan(0).WithMessage("Total value must be greater than 0.");
        RuleFor(x => x.Branch).NotEmpty().WithMessage("Branch is required.");
    }
}
