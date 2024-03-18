using Application.Common.Helpers.Pagination;
using Domain.Entities;
using Domain.Ports;
using Domain.Services;

namespace Application.UseCases.MedicalHistorys.Queries.GetMedicalHistory;

public class PaginationMedicalHistoryQueryHandler : IRequestHandler<PaginationMedicalHistoryQuery,
    ResponsePagination<MedicalHistoryDto>>
{
    private readonly MedicalHistoryService _medicalHistoryService;
    private readonly IGenericRepository<MedicalHistory> _repository;
    private readonly IMapper _mapper;

    public PaginationMedicalHistoryQueryHandler(IGenericRepository<MedicalHistory>? repository,
        MedicalHistoryService medicalHistoryService,
        IMapper mapper) =>
        (_repository, _medicalHistoryService, _mapper) = (repository, medicalHistoryService, mapper);


    public async Task<ResponsePagination<MedicalHistoryDto>> Handle(PaginationMedicalHistoryQuery request,
        CancellationToken cancellationToken)
    {
        // var medicalHistorysPaginated = await _repository.GetPagedAsync(request.Page, request.RecordsPerPage);
        var medicalHistorysPaginated = await _medicalHistoryService.GetAllPaginatedMedicalHistory(request.Page, request.RecordsPerPage);
        var dataPaginated = _mapper.Map<List<MedicalHistoryDto>>(medicalHistorysPaginated.Records);

        
        return new ResponsePagination<MedicalHistoryDto>
        {
            Page = request.Page,
            Records = dataPaginated,
            TotalPages = medicalHistorysPaginated.TotalPages,
            TotalRecords = medicalHistorysPaginated.TotalRecords
        };
    }
}