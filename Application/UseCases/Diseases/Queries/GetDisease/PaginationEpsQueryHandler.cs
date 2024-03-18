using Application.Common.Helpers.Pagination;
using Domain.Entities;
using Domain.Ports;

namespace Application.UseCases.Diseases.Queries.GetDisease;

public class PaginationDiseaseQueryHandler : IRequestHandler<PaginationDiseaseQuery, ResponsePagination<DiseaseDto>>
{
    private readonly IGenericRepository<Disease> _repository;
    private readonly IMapper _mapper;

    public PaginationDiseaseQueryHandler(IGenericRepository<Disease>? repository, IMapper mapper) =>
        (_repository, _mapper) = (repository, mapper);

    public async Task<ResponsePagination<DiseaseDto>> Handle(PaginationDiseaseQuery request,
        CancellationToken cancellationToken)
    {
        var diseasePaginated = await _repository.GetPagedAsync(request.Page, request.RecordsPerPage);
        var dataPaginated = _mapper.Map<List<DiseaseDto>>(diseasePaginated.Records);

        return new ResponsePagination<DiseaseDto>
        {
            Page = request.Page,
            Records = dataPaginated,
            TotalPages = diseasePaginated.TotalPages,
            TotalRecords = diseasePaginated.TotalRecords
        };
    }
}