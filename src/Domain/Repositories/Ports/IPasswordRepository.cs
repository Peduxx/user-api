using Domain.ValueObjects;

namespace Domain.Repositories.Ports
{
    public interface IPasswordRepository
    {
        Task Save(Password password);
        Task<string> GetByUserId(Guid userId);
        Task<byte[]> GetSaltByUserId(Guid userId);
    }
}
