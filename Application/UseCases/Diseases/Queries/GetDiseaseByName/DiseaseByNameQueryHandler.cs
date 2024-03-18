using Application.UseCases.Diseases.Queries.GetDisease;
using Domain.Entities;
using Domain.Ports;
using Domain.Services;

namespace Application.UseCases.Diseases.Queries.GetDiseaseByName;

public class DiseaseByNameQueryHandler : IRequestHandler<DiseaseByNameQuery, DiseaseDto>
{
    private readonly IGenericRepository<Disease> _repository;
    private readonly DiseaseService _diseaseServices;
    private readonly IMapper _mapper;

    public DiseaseByNameQueryHandler(IGenericRepository<Disease>? repository, DiseaseService diseaseServices,
        IMapper mapper) =>
        (_repository, _diseaseServices, _mapper) = (repository, diseaseServices, mapper);

    public async Task<DiseaseDto> Handle(DiseaseByNameQuery request, CancellationToken cancellationToken)
    {
        var diseaseFilterByName = await _diseaseServices.GetByName(request.Name);
        var data = _mapper.Map<DiseaseDto>(diseaseFilterByName);
        return data;
    }
}