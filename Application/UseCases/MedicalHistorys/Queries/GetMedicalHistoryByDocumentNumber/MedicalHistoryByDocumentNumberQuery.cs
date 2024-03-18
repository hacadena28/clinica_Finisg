using Application.Common.Helpers.Pagination;
using Application.UseCases.MedicalHistorys.Queries.GetMedicalHistory;

namespace Application.UseCases.MedicalHistorys.Queries.GetMedicalHistoryByDocumentNumber;

public record MedicalHistoryByDocumentNumberQuery(string DocumentNumber) : RequestPagination, IRequest<ResponsePagination<MedicalHistoryByDocumentNumberDto>>;