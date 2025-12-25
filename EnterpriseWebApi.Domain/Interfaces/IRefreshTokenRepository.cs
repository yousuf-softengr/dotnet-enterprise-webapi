using EnterpriseWebApi.Domain.Entities;

namespace EnterpriseWebApi.Domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token);
    Task<RefreshToken?> GetAsync(string token);
    Task UpdateAsync(RefreshToken token);
}
