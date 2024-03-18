using Domain.Entities;
using Domain.Ports;

namespace Application.UseCases.Diseases.Queries.GetDiseaseNormal;

public class GetAllDiseaseQueryHandler : IRequestHandler<GetAllDiseaseQuery, IEnumerable<DiseaseNormalDto>>
{
    private readonly IGenericRepository<Disease> _repository;
    private readonly IMapper _mapper;

    public GetAllDiseaseQueryHandler(IGenericRepository<Disease> repository, IMapper mapper) =>
        (_repository, _mapper) = (repository, mapper);

    public async Task<IEnumerable<DiseaseNormalDto>> Handle(GetAllDiseaseQuery request,
        CancellationToken cancellationToken)
    {
        var diseasePaginated = await _repository.GetAsync();
        return _mapper.Map<List<DiseaseNormalDto>>(diseasePaginated);
    }
}