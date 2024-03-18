using Domain.Services;

namespace Application.UseCases.Diseases.Commands.DiseaseUpdate;

public class DiseaseUpdateCommandHandler : IRequestHandler<DiseaseUpdateCommand>
{
    private readonly DiseaseService _diseaseService;

    public DiseaseUpdateCommandHandler(DiseaseService diseaseService
    )
    {
        _diseaseService = diseaseService ?? throw new ArgumentNullException(nameof(diseaseService));
    }

    public async Task<Unit> Handle(DiseaseUpdateCommand request, CancellationToken cancellationToken)
    {
        await _diseaseService.Update(request.Id, request.NewName.Trim());
        return Unit.Value;
    }
}