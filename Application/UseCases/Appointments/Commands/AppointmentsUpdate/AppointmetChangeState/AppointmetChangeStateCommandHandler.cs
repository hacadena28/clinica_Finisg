using Domain.Enums;
using Domain.Ports;
using Domain.Services;

namespace Application.UseCases.Appointments.Commands.AppointmentsUpdate.AppointmetChangeState
{
    public class AppointmetChangeStateCommandHandler : IRequestHandler<AppointmetChangeStateCommand>
    {
        private readonly AppointmentService _appointmentService;
        private readonly IEmailService _iEmailService;
        private readonly IGenericRepository<Domain.Entities.Appointment> _appointmentRepository;

        public AppointmetChangeStateCommandHandler(AppointmentService appointmentService,
            IEmailService iEmailService,
            IGenericRepository<Domain.Entities.Appointment> appointmentRepository)
        {
            _iEmailService = iEmailService ?? throw new ArgumentNullException(nameof(iEmailService));
            _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
            _appointmentRepository =
                appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
        }

        public async Task<Unit> Handle(AppointmetChangeStateCommand request,
            CancellationToken cancellationToken)
        {
            var appointmentSearched = (await _appointmentRepository.GetAsync(e => e.Id == request.Id,includeStringProperties:"Patient")).FirstOrDefault();
           
            string content = $@"
    <!DOCTYPE html>
    <html lang=""en"">
    <head>
      <meta charset=""UTF-8"">
      <title>Notificación de cambio de estado de cita</title>
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
        <h2>Cambio de estado de cita</h2>
        <p>¡Hola {appointmentSearched.Patient.FirstName} {appointmentSearched.Patient.LastName}!</p>
        <p>Te informamos que tu cita para el {appointmentSearched.AppointmentStartDate} ha cambiado de estado a: {request.State}.</p>";
            if (request.State.Equals(AppointmentState.Rescheduled))
            {
                content += $@"<div>
                    <p>La nueva fecha es: {request.NewDate}</p>
                </div>";
            }

            content += @"
            </div>
            </body>
            </html>";
            
            await _appointmentService.ChangeStateAppointment(
                request.Id, request.State,
                request.NewDate);

            await _iEmailService.SendEmail("Cambio de estado de la cita", appointmentSearched.Patient.Email, content);
            return Unit.Value;
        }
    }
}