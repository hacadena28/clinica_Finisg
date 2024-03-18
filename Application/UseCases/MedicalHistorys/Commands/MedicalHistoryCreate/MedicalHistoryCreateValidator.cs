namespace Application.UseCases.MedicalHistorys.Commands.MedicalHistoryCreate;

public class MedicalHistoryCreateValidator : AbstractValidator<MedicalHistoryCreateCommand>
{
    public MedicalHistoryCreateValidator()
    {
        RuleFor(_ => _.Description).NotNull().NotEmpty().MinimumLength(1).MaximumLength(250);
        RuleFor(_ => _.Treatment).NotNull().NotEmpty().MinimumLength(1).MaximumLength(250);
        RuleFor(_ => _.PatiendId).NotNull().NotEmpty();
    }

    private bool BeAValidDate(DateTime date)
    {
        return !date.Equals(default(DateTime));
    }
}