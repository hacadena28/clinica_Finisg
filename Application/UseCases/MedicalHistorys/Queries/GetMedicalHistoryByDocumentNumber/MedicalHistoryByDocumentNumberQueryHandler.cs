using Application.Common.Helpers.Pagination;
using Application.UseCases.MedicalHistorys.Queries.GetMedicalHistory;
using Application.UseCases.MedicalHistorys.Queries.GetMedicalHistoryByPatientId;
using Domain.Entities;
using Domain.Ports;
using Domain.Services;

namespace Application.UseCases.MedicalHistorys.Queries.GetMedicalHistoryByDocumentNumber;

public class
    MedicalHistoryByDocumentNumberQueryHandler : IRequestHandler<MedicalHistoryByDocumentNumberQuery,
        ResponsePagination<MedicalHistoryByDocumentNumberDto>>
{
    private readonly IGenericRepository<User> _repository;
    private readonly MedicalHistoryService _medicalHistoryServices;
    private readonly IMapper _mapper;

    public MedicalHistoryByDocumentNumberQueryHandler(IGenericRepository<User> repository,
        MedicalHistoryService medicalHistoryServices,
        IMapper mapper) =>
        (_repository, _medicalHistoryServices, _mapper) = (repository, medicalHistoryServices, mapper);

    public async Task<ResponsePagination<MedicalHistoryByDocumentNumberDto>> Handle(
        MedicalHistoryByDocumentNumberQuery request,
        CancellationToken cancellationToken)
    {
        var medicalHistoryFilterByPatientId =
            await _medicalHistoryServices.GetByPatientDocumentNumber(request.DocumentNumber, request.Page,
                request.RecordsPerPage);
        var data = new List<MedicalHistoryByDocumentNumberDto>();
        
        data = _mapper.Map<List<MedicalHistoryByDocumentNumberDto>>(medicalHistoryFilterByPatientId.Records);

        // foreach (var medicalHistory in data)
        // {
        //     var medical = new MedicalHistoryByDocumentNumberDto(
        //         medicalHistory.Id,
        //         medicalHistory.Date,
        //         medicalHistory.Description,
        //         medicalHistory.Diagnosis,
        //         medicalHistory.Treatment,
        //         medicalHistory.PatientId,
        //         medicalHistory.Patient.DocumentNumber,
        //         $"{medicalHistory.Patient.FirstName}  {medicalHistory.Patient.SecondName} {medicalHistory.Patient.LastName} {medicalHistory.Patient.SecondLastName}"
        //     );
        //     data.Add(medical);
        // }
        // // var data2 = _mapper.Map<List<MedicalHistoryByDocumentNumberDto>>(medicalHistoryFilterByPatientId.Records);

        return new ResponsePagination<MedicalHistoryByDocumentNumberDto>
        {
            Page = request.Page,
            Records = data,
            TotalPages = medicalHistoryFilterByPatientId.TotalPages,
            TotalRecords = medicalHistoryFilterByPatientId.TotalRecords
        };
    }
}