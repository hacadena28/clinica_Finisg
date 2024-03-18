namespace Application.UseCases.Diseases.Commands.DiseaseCreate;

public class DiseaseCreateValidator : AbstractValidator<DiseaseCreateCommand>
{
    public DiseaseCreateValidator()
    {
        RuleFor(_ => _.Name).NotNull().NotEmpty().MinimumLength(2).MaximumLength(40);
    }
}