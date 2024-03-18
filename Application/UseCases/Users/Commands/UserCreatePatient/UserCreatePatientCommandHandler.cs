using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Ports;
using Domain.Services;
using Xunit.Sdk;

namespace Application.UseCases.Users.Commands.UserCreatePatient;

public class UserCreatePatientCommandHandler : IRequestHandler<UserCreatePatientCommand>
{
    private readonly UserService _userService;
    private readonly PatientService _patientService;
    private readonly IGenericRepository<Patient> _patientRepository;
    private readonly IGenericRepository<Eps> _epsRepository;
    private readonly IEmailService _emailService;

    public UserCreatePatientCommandHandler(UserService userService, PatientService patientService,
        IGenericRepository<Patient> patientRepository,
        IGenericRepository<Eps> epsRepository, IEmailService emailService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
        _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        _epsRepository = epsRepository ?? throw new ArgumentNullException(nameof(epsRepository));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }

    public async Task<Unit> Handle(UserCreatePatientCommand request, CancellationToken cancellationToken)
    {
        var eps = await _epsRepository.GetByIdAsync(request.Patient.EpsId);
        if (eps == null)
        {
            throw new EntityNotFound(Messages.EntityNotFound);
        }

        var patient = new Patient(
            request.Patient.FirstName.Trim(),
            request.Patient.SecondName.Trim(),
            request.Patient.LastName.Trim(),
            request.Patient.SecondLastName.Trim(),
            request.Patient.DocumentType,
            request.Patient.DocumentNumber.Trim(),
            request.Patient.Email.Trim(),
            request.Patient.Phone.Trim(),
            request.Patient.Address.Trim(),
            request.Patient.Birthdate.AddYears(-19),
            request.Patient.EpsId
        );

        var searchedDoctor = await _patientService.GetPatientByDocumentNumber(request.Patient.DocumentNumber);
        if (searchedDoctor != null)
        {
            throw new AlreadyExistException(Domain.Messages.AlredyExistException);
        }

        string content = $@"
                <!DOCTYPE html>
                <html lang=""en"">
                <head>
                  <meta charset=""UTF-8"">
                  <title>Notificación de creación de usuario</title>
                  <style>
                    /* Estilos para la notificación */
                    body {{
                      font-family: Arial, sans-serif;
                      background-color: #f4f4f4;
                      margin: 0;
                      padding: 20px;
                    }}
                    .notification {{
                      background-color: #4CAF50;
                      color: white;
                      text-align: center;
                      padding: 20px;
                    }}
                  </style>
                </head>
                <body>
                  <div class=""notification"">
                    <h2>Usuario {request.Patient.FirstName} {request.Patient.LastName} creado correctamente</h2>
                    <p>¡Tu usuario ha sido creado exitosamente!</p>
                  </div>
                </body>
                </html>";

        var user = new User(request.Password, Role.Patient, patient);
        await _patientRepository.AddAsync(patient);
        await _userService.CreateUser(user);
        await _emailService.SendEmail("Usuario Creado correctamente", request.Patient.Email, content);
        return new Unit();
    }
}