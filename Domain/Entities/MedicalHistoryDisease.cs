using System.Text.Json.Serialization;
using Domain.Enums;

namespace Domain.Entities;

public class MedicalHistoryDisease : EntityBase<Guid>
{
    public Guid MedicalHistoryId { get; set; }
    public MedicalHistory MedicalHistory { get; set; }

    public Guid DiseaseId { get; set; }
    public Disease Disease { get; set; }

    public MedicalHistoryDisease(Guid medicalHistoryId, MedicalHistory medicalHistory, Guid diseaseId, Disease disease)
    {
        MedicalHistoryId = medicalHistoryId;
        MedicalHistory = medicalHistory;
        DiseaseId = diseaseId;
        Disease = disease;
    }

    public MedicalHistoryDisease(Guid medicalHistoryId, Guid diseaseId)
    {
        MedicalHistoryId = medicalHistoryId;
        DiseaseId = diseaseId;
    }

    public MedicalHistoryDisease()
    {
    }
}