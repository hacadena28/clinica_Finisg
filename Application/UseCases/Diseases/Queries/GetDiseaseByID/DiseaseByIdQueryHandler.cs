using Application.UseCases.Diseases.Queries.GetDisease;
using Domain.Entities;
using Domain.Ports;
using Domain.Services;

namespace Application.UseCases.Diseases.Queries.GetDiseaseByID;

public class DiseaseByIdQueryHandler : IRequestHandler<DiseaseByIdQuery, DiseaseDto>
{
    private readonly IGenericRepository<Disease> _repository;
    private readonly DiseaseService _diseaseServices;
    private readonly IMapper _mapper;

    public DiseaseByIdQueryHandler(IGenericRepository<Disease>? repository, DiseaseService diseaseServices,
        IMapper mapper) =>
        (_repository, _diseaseServices, _mapper) = (repository, diseaseServices, mapper);

    public async Task<DiseaseDto> Handle(DiseaseByIdQuery request, CancellationToken cancellationToken)
    {
        var diseaseFilterById = await _diseaseServices.GetById(request.Id);
        var data = _mapper.Map<DiseaseDto>(diseaseFilterById);
        return data;
    }
}