using Domain.Entities;
using Domain.Enums;

namespace Application.UseCases.MedicalHistorys.Commands.MedicalHistoryCreate;

public record MedicalHistoryCreateCommand(
    DateTime Date,
    string Description,
    List<string> Diagnosis,
    string Treatment,
    Guid PatiendId
) : IRequest;