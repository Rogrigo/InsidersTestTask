using InsidersTestTask.Data;
using InsidersTestTask.Interfaces;
using InsidersTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace InsidersTestTask.Repository
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly ApplicationDbContext _context;

        public CryptoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CryptoInfo>> GetAllCryptosAsync()
        {
            return await _context.CryptoInfos.ToListAsync();
        }
        public async Task DeleteCryptoAsync(string id)
        {
            var crypto = await _context.CryptoInfos.FindAsync(id);
            if (crypto != null)
            {
                _context.CryptoInfos.Remove(crypto);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<CryptoInfo> GetCryptoByIdAsync(string id)
        {
            return await _context.CryptoInfos.FindAsync(id);
        }

        public async Task<bool> UpdateCryptoAsync(string id, CryptoInfo updatedCryptoInfo)
        {
            var cryptoInfo = await _context.CryptoInfos.FindAsync(id);
            if (cryptoInfo == null)
            {
                return false;
            }
            _context.Entry(cryptoInfo).CurrentValues.SetValues(updatedCryptoInfo);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
