using System.Linq.Expressions;
using Domain.Entities;
using Domain.Ports;

namespace Domain.Services;

[DomainService]
public class MedicalHistoryService
{
    private readonly IGenericRepository<MedicalHistory> _medicalHistoryRepository;

    public MedicalHistoryService(IGenericRepository<MedicalHistory> medicalHistoryRepository)
    {
        _medicalHistoryRepository = medicalHistoryRepository;
    }

    public async Task Create(MedicalHistory medicalHistory)
    {
        await _medicalHistoryRepository.AddAsync(medicalHistory);
    }

    public async Task Update(Guid medicalHistoryId, string? description, string? diagnosis, string? treatment)
    {
        var existingMedicalHistory = _medicalHistoryRepository.GetByIdAsync(medicalHistoryId);
        // existingMedicalHistory.Update(description, diagnosis, treatment);
    }

    public async Task<MedicalHistory> GetById(Guid id)
    {
        var result = await _medicalHistoryRepository.GetByIdAsync(id, filter: a => a.Id == id,
            isTracking: true,
            includeObjectProperties: q => q.Diagnosis,
            includeStringProperties: "Diagnosis.Disease");
        return result;
    }

    public async Task<PagedResult<MedicalHistory>> GetAllPaginatedMedicalHistory(int page, int pageSize)
    {
        var result = await _medicalHistoryRepository.GetPagedAsync(
            page,
            pageSize,
            filter: a => a.PatientId != null,
            isTracking: true,
            includeObjectProperties: new Expression<Func<MedicalHistory, object>>[]
            {
                q => q.Diagnosis,
                q => q.Patient
            },
            includeStringProperties: "Diagnosis.Disease,Patient");
        return result;
    }

    public async Task<IEnumerable<MedicalHistory>> GetAllMedicalHistoryByPatientId(Guid patientId)
    {
        var result = await _medicalHistoryRepository.GetAsync(
            filter: a => a.PatientId == patientId,
            isTracking: true);
        return result;
    }

    public async Task<PagedResult<MedicalHistory>> GetByPatientId(Guid patientId, int page, int pageSize)
    {
        var result = await _medicalHistoryRepository.GetPagedFilterAsync(
            page: page,
            pageSize: pageSize,
            filter: e => e.PatientId == patientId,
            orderBy: query => query.OrderByDescending(e => e.Date),
            includeObjectProperties: m => m.Diagnosis,
            includeStringProperties: "Diagnosis.Disease,Patient",
            isTracking: false);

        return result;
    }

    public async Task<PagedResult<MedicalHistory>> GetByPatientDocumentNumber(string documentNumber, int page,
        int pageSize)
    {
        var result = await _medicalHistoryRepository.GetPagedFilterAsync(
            page: page,
            pageSize: pageSize,
            filter: e => e.Patient.DocumentNumber == documentNumber,
            orderBy: query => query.OrderByDescending(e => e.Date),
            includeObjectProperties: m => m.Diagnosis,
            includeStringProperties: "Diagnosis.Disease,Patient",
            isTracking: false);

        return result;
    }
}