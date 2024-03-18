namespace Application.UseCases.Diseases.Commands.DiseaseUpdate
{
    public record DiseaseUpdateCommand(Guid Id, string NewName) : IRequest<Unit>;
}