
namespace Application.UseCases.Diseases.Queries.GetDiseaseNormal;

public record GetAllDiseaseQuery : IRequest<IEnumerable<DiseaseNormalDto>>;