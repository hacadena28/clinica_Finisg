using Application.UseCases.Users.Commands.UserDelete;
using Domain.Entities;
using Domain.Enums;
using Domain.Ports;
using Domain.Services;

namespace Application.UseCases.Users.Commands.UserRecoveryPassword
{
    public class UserRecoveryPasswordCommandHandler : IRequestHandler<UserRecoveryPasswordCommand>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly RecoverPasswordService _recoverPasswordService;

        public UserRecoveryPasswordCommandHandler(
            IGenericRepository<User> userRepository,
            RecoverPasswordService recoverPasswordService
            )
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _recoverPasswordService =
                recoverPasswordService ?? throw new ArgumentNullException(nameof(recoverPasswordService));
        }

        public async Task<Unit> Handle(UserRecoveryPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = (await _userRepository.GetAsync(u => u.Person.DocumentNumber== request.DocumentNumber,includeStringProperties:"Person")).FirstOrDefault();


            await _recoverPasswordService.SendEmailToRecoverPassword(user.Person.DocumentNumber);
            
            return new Unit();
        }
    }
}