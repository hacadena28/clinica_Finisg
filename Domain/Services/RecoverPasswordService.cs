using Domain.Entities;
using Domain.Ports;

namespace Domain.Services;

[DomainService]
public class RecoverPasswordService
{
    private readonly IGenericRepository<User> _repository;
    private readonly IEmailService _emailService;
    public RecoverPasswordService(IGenericRepository<User> repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    public async Task SendEmailToRecoverPassword(string identification)
    {
        var user = (await _repository.GetAsync(u => u.Person.DocumentNumber == identification,
            includeStringProperties: "Person")).FirstOrDefault();

        if (user == null)
        {
            throw new Exception();
        }

        string content = $@"
    <!DOCTYPE html>
    <html lang=""en"">
    <head>
      <meta charset=""UTF-8"">
      <title>Recuperacion de contraseña</title>
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
        <h2>Recuperacion de contraseña</h2>
        <p>¡Hola {user.Person.FirstName} {user.Person.LastName}!</p>
        <p>Tu contraseña para el usuario {user.Person.DocumentNumber} es {user.Password}</p>
            </div>
            </body>
            </html>";

        await _emailService.SendEmail("Recuperacion de contraseña", user.Person.Email, content);
    }
}