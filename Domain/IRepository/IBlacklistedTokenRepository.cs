using Domain.Entities;

namespace Domain.IRepository
{
    public interface IBlacklistedTokenRepository
    {
        void Add(BlacklistedToken token);
        bool IsTokenBlacklisted(string token);
        void RemoveExpiredTokens();
    }

}
