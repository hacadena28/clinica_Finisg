using System.Data;
using Domain.Ports;
using Infrastructure.Adapters;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions.Email;

public static class EmailExtensions
{
    public static IServiceCollection AddEmailService(this IServiceCollection svc)
    {
        svc.AddScoped(typeof(IEmailService), typeof(EmailService));
        return svc;
    }
}