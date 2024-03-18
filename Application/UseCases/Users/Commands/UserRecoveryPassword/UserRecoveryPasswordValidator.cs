using Application.UseCases.Users.Commands.UserDelete;

namespace Application.UseCases.Users.Commands.UserRecoveryPassword;

public class UserRecoveryPasswordValidator : AbstractValidator<UserRecoveryPasswordCommand>
{
    public UserRecoveryPasswordValidator()
    {
        RuleFor(_ => _.DocumentNumber).NotEmpty().NotNull();
    }
}