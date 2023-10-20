using Domain.Entities;
using Domain.IRepository;

namespace Infraestructure.Repository
{
    public class BlacklistedTokenInMemoryRepository : IBlacklistedTokenRepository
    {
        private readonly List<BlacklistedToken> _blacklistedTokens = new List<BlacklistedToken>();

        public void Add(BlacklistedToken token)
        {
            _blacklistedTokens.Add(token);
        }

        public bool IsTokenBlacklisted(string token)
        {
            return _blacklistedTokens.Any(t => t.Token == token && t.ExpiryDate > DateTime.UtcNow);
        }

        public void RemoveExpiredTokens()
        {
            _blacklistedTokens.RemoveAll(t => t.ExpiryDate <= DateTime.UtcNow);
        }
    }
}