using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Services;

namespace Application.UseCases.Diseases.Commands.DiseaseCreate;

public class DiseaseCreateCommandHandler : IRequestHandler<DiseaseCreateCommand>
{
    private readonly DiseaseService _diseaseService;

    public DiseaseCreateCommandHandler(DiseaseService diseaseService)
    {
        _diseaseService = diseaseService ?? throw new ArgumentNullException(nameof(diseaseService));
    }

    public async Task<Unit> Handle(DiseaseCreateCommand request, CancellationToken cancellationToken)
    {
        var searchedDisease = await _diseaseService.GetByName(request.Name.Trim());
        if (searchedDisease != null)
        {
            throw new AlreadyExistException(Domain.Messages.AlredyExistException);
        }

        var disease = new Disease(request.Name.Trim());
        await _diseaseService.CreateDisease(disease);
        return Unit.Value;
    }
}