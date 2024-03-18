using Application.UseCases.MedicalHistorys.Queries.GetMedicalHistory;
using Domain.Entities;

namespace Application.UseCases.MedicalHistorys.Queries.GetMedicalHistoryByDocumentNumber;

public record PatientDocumentNumberDto(string FirstName, string SecondName, string LastName, string SecondLastName,
    string DocumentNumber);
public record MedicalHistoryByDocumentNumberDto(Guid Id, DateTime Date, string Description, List<MedicalHistoryDiseaseDto> Diagnosis, string Treatment,
    Guid PatientId, PatientDocumentNumberDto Patient);