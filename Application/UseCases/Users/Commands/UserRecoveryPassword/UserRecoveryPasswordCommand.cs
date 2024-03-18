namespace Application.UseCases.Users.Commands.UserRecoveryPassword;

public record UserRecoveryPasswordCommand(string DocumentNumber) : IRequest;