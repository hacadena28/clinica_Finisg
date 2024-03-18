using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Domain.Ports;

namespace Infrastructure.Adapters;

public class EmailService : IEmailService
{
    private MailMessage Message { get; set; } = new();

    public Task SendEmail(string subject, string emailToReceive, string content)
    {
        const string emailEmisor = "hagustincodazzi@gmail.com";
        var smtp = new SmtpClient();
        Message.From = new MailAddress(emailEmisor);
        Message.To.Add(new MailAddress(emailToReceive));
        Message.Subject = subject;
        Message.Body = content;
        var view =
            AlternateView.CreateAlternateViewFromString(content, Encoding.UTF8, MediaTypeNames.Text.Html);
        Message.AlternateViews.Add(view);
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 587;
        smtp.Credentials = new NetworkCredential(emailEmisor, "tthvfzhjeaakoolu");
        smtp.EnableSsl = true;
        
        try
        {
            smtp.Send(Message);
            Console.WriteLine("Se envio correctamente");
        }
        catch (Exception ex)
        {
            Console.WriteLine("No Se envio correctamente", ex.Message);
            throw;
        }

        return Task.CompletedTask;
    }
}