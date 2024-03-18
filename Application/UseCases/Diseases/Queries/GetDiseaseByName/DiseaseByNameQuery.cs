using Application.UseCases.Diseases.Queries.GetDisease;

namespace Application.UseCases.Diseases.Queries.GetDiseaseByName;

public record DiseaseByNameQuery(string Name) : IRequest<DiseaseDto>;