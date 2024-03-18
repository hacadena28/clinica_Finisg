namespace Application.UseCases.Diseases.Commands.DiseaseUpdate
{
    public class DiseaseUpdateValidator : AbstractValidator<DiseaseUpdateCommand>
    {
        public DiseaseUpdateValidator()
        {
            RuleFor(_ => _.Id).NotEmpty().WithMessage("El campo Valor no puede estar vacío.")
                .Must(_ => _ == null || _ is Guid).WithMessage("El campo Valor debe ser de tipo Guid.");
            RuleFor(_ => _.NewName).NotNull().NotEmpty().MinimumLength(2).MaximumLength(40);
        }
    }
}