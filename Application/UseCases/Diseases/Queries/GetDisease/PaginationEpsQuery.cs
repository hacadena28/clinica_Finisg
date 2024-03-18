using Application.Common.Helpers.Pagination;

namespace Application.UseCases.Diseases.Queries.GetDisease;

public record PaginationDiseaseQuery : RequestPagination, IRequest<ResponsePagination<DiseaseDto>>;