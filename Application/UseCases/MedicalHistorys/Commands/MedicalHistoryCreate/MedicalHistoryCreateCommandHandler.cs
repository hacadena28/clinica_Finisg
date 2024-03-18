using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Ports;
using Domain.Services;

namespace Application.UseCases.MedicalHistorys.Commands.MedicalHistoryCreate;

public class MedicalHistoryCreateCommandHandler : IRequestHandler<MedicalHistoryCreateCommand>
{
    private readonly MedicalHistoryService _medicalHistoryServices;
    private readonly IGenericRepository<Patient> _patientRepository;
    private readonly IGenericRepository<Disease> _diseaseRepository;

    public MedicalHistoryCreateCommandHandler(MedicalHistoryService medicalHistoryServices,
        IGenericRepository<Patient> patientRepository, IGenericRepository<Disease> diseaseRepository)
    {
        _medicalHistoryServices =
            medicalHistoryServices ?? throw new ArgumentNullException(nameof(medicalHistoryServices));
        _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        _diseaseRepository = diseaseRepository ?? throw new ArgumentNullException(nameof(diseaseRepository));
    }

    public async Task<Unit> Handle(MedicalHistoryCreateCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetAsync(filter: e => e.Id == request.PatiendId);

        if (patient == null)
        {
            throw new NotFoundException(Messages.EntityNotFound);
        }

        List<MedicalHistoryDisease> list = new();
        var medicalHistory = new MedicalHistory();

        if (request.Diagnosis.Count != 0)
        {
            foreach (var disease in request.Diagnosis)
            {
                var d = await _diseaseRepository.GetAsync(d => d.Name == disease);
                if (d != null)
                {
                    list.Add(new MedicalHistoryDisease(medicalHistory.Id, d.FirstOrDefault().Id));
                }
            }
        }

        medicalHistory.Date
            = request.Date;
        medicalHistory.Description =
            request.Description;
        medicalHistory.Diagnosis =
            list;
        medicalHistory.Treatment =
            request.Treatment;
        medicalHistory.PatientId =
            patient.FirstOrDefault().Id;

        await _medicalHistoryServices.Create(medicalHistory);

        return Unit.Value;
    }
}