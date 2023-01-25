namespace Backend.Validation.Validators;

public class PlayerModelValidator : AbstractValidator<PlayerModel>
{
    public PlayerModelValidator()
    {
        RuleFor(x => x.Birthday).NotEmpty()
                                .NotNull();

        RuleFor(x => x.BirthPlace).NotEmpty()
                                  .NotNull();

        RuleFor(x => x.Club).NotEmpty()
                            .NotNull();

        RuleFor(x => x.Description).NotEmpty()
                                   .NotNull();

        RuleFor(x => x.Height).NotEmpty()
                              .NotNull()
                              .GreaterThan(0);

        RuleFor(x => x.Weight).NotEmpty()
                              .NotNull()
                              .GreaterThan(0);

        RuleFor(x => x.Position).NotNull()
                                .SetValidator(new PositionModelValidator());
    }
}
