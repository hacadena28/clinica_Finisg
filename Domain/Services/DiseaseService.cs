using Domain.Entities;
using Domain.Exceptions;
using Domain.Ports;

namespace Domain.Services;

[DomainService]
public class DiseaseService
{
    private readonly IGenericRepository<Disease> _diseaseRepository;

    public DiseaseService(IGenericRepository<Disease> diseaseRepository)
    {
        _diseaseRepository = diseaseRepository;
    }

    public async Task CreateDisease(Disease disease)
    {
        await _diseaseRepository.AddAsync(disease);
    }

    public async Task<Disease> GetById(Guid diseaseId)
    {
        return await _diseaseRepository.GetByIdAsync(diseaseId);
    }

    public async Task<Disease> GetByName(string name)
    {
        var result = await _diseaseRepository.GetPagedFilterAsync(
            page: 1,
            pageSize: 20,
            filter: e => e.Name == name,
            orderBy: null,
            includeStringProperties: "",
            isTracking: false);

        return result.Records.FirstOrDefault();
    }

    public async Task Update(Guid diseaseId, string newName)
    {
        var disease = await _diseaseRepository.GetByIdAsync(diseaseId);
        _ = disease ?? throw new CoreBusinessException(Messages.ResourceNotFoundException);
        disease.Update(newName);
        await _diseaseRepository.UpdateAsync(disease);
    }

    public async Task DeleteDisease(Disease disease)
    {
        await _diseaseRepository.DeleteAsync(disease);
    }
}