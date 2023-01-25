namespace Backend.Validation.Validators;

public class PositionModelValidator : AbstractValidator<PositionModel>
{
    public PositionModelValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.Name).NotEmpty()
                            .NotNull()
                            .MaximumLength(255);
    }
}
