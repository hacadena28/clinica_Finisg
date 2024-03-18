namespace Application.UseCases.Diseases.Commands.DiseaseCreate;

public record DiseaseCreateCommand(
    string Name
) : IRequest<Unit>;