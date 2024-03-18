namespace Application.UseCases.Diseases.Commands.DiseaseDelete
{
    public record DiseaseDeleteCommand(Guid Id) : IRequest<Unit>;
}