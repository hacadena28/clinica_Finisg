using System.Data;
using Domain.Enums;

namespace Domain.Entities;

public class MedicalHistory : EntityBase<Guid>
{
    public DateTime Date { get; set; }


    public string Description { get; set; }
    public List<MedicalHistoryDisease> Diagnosis { get; set; }
    public string Treatment { get; set; }
    public Guid PatientId { get; set; }
    public virtual Patient Patient { get; set; }


    public MedicalHistory
    (
        DateTime date,
        string description,
        List<MedicalHistoryDisease> diagnosis,
        string treatment,
        Guid patientId
    )
    {
        Date = date;
        Description = description;
        Diagnosis = diagnosis;
        Treatment = treatment;
        PatientId = patientId;
    }

    public MedicalHistory()
    {
    }

    public void Update(string? description, string? treatment)
    {
        if (description != null && !Description.Equals(description) && description != "") Description = description;
        if (treatment != null && !Treatment.Equals(treatment) && treatment != "") Treatment = treatment;
    }

    public void UpdateDiagnosis(List<MedicalHistoryDisease> newDiagnosis)
    {
        if (newDiagnosis != null) Diagnosis = newDiagnosis;
    }
}