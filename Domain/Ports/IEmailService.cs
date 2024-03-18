namespace Domain.Ports;

public interface IEmailService
{
    Task SendEmail(string subject, string emailToReceive, string content);
}