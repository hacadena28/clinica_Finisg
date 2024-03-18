
using Application.UseCases.Diseases.Queries.GetDisease;

namespace Application.UseCases.Diseases.Queries.GetDiseaseByID;

public record DiseaseByIdQuery(Guid Id) : IRequest<DiseaseDto>;