using Domain.Entities;

namespace Application.UseCases.MedicalHistorys.Queries.GetMedicalHistory;

public record DiseasesDto(string Name);

public record MedicalHistoryDiseaseDto(Guid MedicalHistoryId, Guid DiseaseId, DiseasesDto Disease);

public record MedicalHistoryDto(Guid Id, DateTime Date, string Description, List<MedicalHistoryDiseaseDto> Diagnosis,
    string Treatment,
    Guid PatientId);