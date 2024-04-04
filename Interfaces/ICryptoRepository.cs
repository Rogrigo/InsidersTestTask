using InsidersTestTask.Models;

namespace InsidersTestTask.Interfaces
{
    public interface ICryptoRepository
    {
        Task<IEnumerable<CryptoInfo>> GetAllCryptosAsync();
        Task<bool> UpdateCryptoAsync(string id, CryptoInfo updatedCryptoInfo);
        Task DeleteCryptoAsync(string id);
        Task<CryptoInfo> GetCryptoByIdAsync(string id);
    }
}
