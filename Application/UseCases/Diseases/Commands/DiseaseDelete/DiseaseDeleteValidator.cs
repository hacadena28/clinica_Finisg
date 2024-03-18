
namespace Application.UseCases.Diseases.Commands.DiseaseDelete;

public class DiseaseDeleteValidator : AbstractValidator<DiseaseDeleteCommand>
{
    public DiseaseDeleteValidator()
    {
        RuleFor(_ => _.Id).NotEmpty().WithMessage("El campo Valor no puede estar vacío.")
            .Must(value => value == null || value is Guid).WithMessage("El campo Valor debe ser de tipo Guid.");
    }
}