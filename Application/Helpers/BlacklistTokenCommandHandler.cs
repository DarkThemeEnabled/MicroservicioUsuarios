using Application.Interfaces;
using Domain.Entities;
using Domain.IRepository;

namespace Application.Helpers
{
    public class BlacklistTokenCommandHandler: IBlacklistTokenCommandHandler
    {
        private readonly IBlacklistedTokenRepository _repository;

        public BlacklistTokenCommandHandler(IBlacklistedTokenRepository repository)
        {
            _repository = repository;
        }

        public void Handle(BlacklistTokenCommand command)
        {
            var token = new BlacklistedToken
            {
                Token = command.Token,
                ExpiryDate = DateTime.UtcNow.AddHours(1) // Asume que el token original expira en 1 hora.
            };
            _repository.Add(token);
        }
    }
}