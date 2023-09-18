using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Domain.DTO;

namespace Infrastructure.Security.Command
{
    public class AccountLockCommandHandler
    {
        public class LockAccountCommand
        {
            public string Username { get; set; }
            public DateTime LockedUntil { get; set; }
        }

        public class AccountLockHandler : IAccountLockCommandHandler
        {
            private readonly UsuarioContext _dbContext;

            public AccountLockHandler(UsuarioContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task HandleAsync(LockAccountCommand command)
            {
                var user = await _dbContext.Usuarios.SingleOrDefaultAsync(u => u.Username == command.Username);

                if (user != null)
                {
                    var usuarioBloqueadoDTO = new UsuarioBloqueadoDTO
                    {
                        IsLocked = true,
                        LockedUntil = command.LockedUntil
                    };

                    // Guarda la información de bloqueo en una tabla o estructura separada
                    // Puedes tener una tabla "CuentasBloqueadas" o una estructura de datos similar
                    // Guarda la información de bloqueo aquí, en lugar de modificar la entidad Usuario

                    await _dbContext.SaveChangesAsync();
                }
            }
        }
        public interface IAccountLockCommandHandler
        {
            Task HandleAsync(LockAccountCommand command);
        }
    }
}