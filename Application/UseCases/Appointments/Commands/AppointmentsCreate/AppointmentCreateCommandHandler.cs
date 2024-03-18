using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Ports;
using Domain.Services;

namespace Application.UseCases.Appointments.Commands.AppointmentsCreate;

public class AppointmentCreateCommandHandler : IRequestHandler<AppointmentCreateCommand>
{
    private readonly AppointmentService _serviceAppointment;
    private readonly IGenericRepository<User> _userRepository;
    private readonly IGenericRepository<Doctor> _doctorRepository;
    private readonly IEmailService _emailService;

    public AppointmentCreateCommandHandler(AppointmentService serviceAppointment, IEmailService emailService,
        IGenericRepository<User> patientRepository, IGenericRepository<Doctor> doctorRepository)
    {
        _serviceAppointment = serviceAppointment ?? throw new ArgumentNullException(nameof(serviceAppointment));
        _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
        _userRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }

    public async Task<Unit> Handle(AppointmentCreateCommand request, CancellationToken cancellationToken)
    {
        var user = (await _userRepository.GetAsync(x => x.Id == request.UserId, includeStringProperties: "Person"))
            .FirstOrDefault();
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);

        if (doctor == null || user == null)
        {
            throw new NotFoundException(Domain.Messages.ResourceNotFoundException);
        }

        var appointment = new Appointment
        (
            request.AppointmentStartDate,
            request.Type,
            request.Description,
            user.Person.Id,
            request.DoctorId
        );

        string content = $@"
    <!DOCTYPE html>
    <html lang=""en"">
    <head>
      <meta charset=""UTF-8"">
      <title>Notificación de registro de cita</title>
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
        <h2>Registro de cita</h2>
        <p>¡Hola {user.Person.FirstName} {user.Person.LastName}!</p>
        <p>Te informamos que tu cita para el {appointment.AppointmentStartDate} se ha registrado.</p>
            </div>
            </body>
            </html>";

        await _serviceAppointment.Create(appointment);
        await _emailService.SendEmail("Cita registrada", user.Person.Email, content);

        return Unit.Value;
    }
}