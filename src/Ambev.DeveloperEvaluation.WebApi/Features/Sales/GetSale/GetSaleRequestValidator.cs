using FluentValidation;

public class GetSaleRequestValidator : AbstractValidator<GetSaleRequest>
{
    public GetSaleRequestValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("Id must not be an empty GUID.");
    }
}
