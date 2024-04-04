using InsidersTestTask.Data;
using InsidersTestTask.DTO;
using InsidersTestTask.Models;
using Newtonsoft.Json;

namespace InsidersTestTask.Services
{
    public class CryptoService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;

        public CryptoService(HttpClient httpClient, ApplicationDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<List<CryptoDTO>> GetCryptosFromApiAsync()
        {
            var response = await _httpClient.GetAsync("https://api.coincap.io/v2/assets");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<CryptoApiResponseToGetAll>(content);
                return apiResponse?.Data ?? new List<CryptoDTO>();
            }
            return new List<CryptoDTO>();
        }


        public async Task<CryptoDTO> GetCryptoByIdFromApiAsync(string id)
        {
            var response = await _httpClient.GetAsync($"https://api.coincap.io/v2/assets/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var wrapper = JsonConvert.DeserializeObject<CryptoApiResponse>(content);
                return wrapper?.Data;
            }
            return null;
        }

        public async Task SaveCryptoInfoAsync(CryptoDTO cryptoDto)
        {
            var existingCrypto = _context.CryptoInfos.Find(cryptoDto.Id);
            if (existingCrypto != null)
            {
                _context.Entry(existingCrypto).CurrentValues.SetValues(cryptoDto);
            }
            else
            {
                var cryptoInfo = new CryptoInfo
                {
                    Id = cryptoDto.Id,
                    Rank = cryptoDto.Rank,
                    Symbol = cryptoDto.Symbol,
                    Name = cryptoDto.Name,
                    Supply = cryptoDto.Supply,
                    MaxSupply = cryptoDto.MaxSupply,
                    MarketCapUsd = cryptoDto.MarketCapUsd,
                    VolumeUsd24Hr = cryptoDto.VolumeUsd24Hr,
                    PriceUsd = cryptoDto.PriceUsd,
                    ChangePercent24Hr = cryptoDto.ChangePercent24Hr,
                    Vwap24Hr = cryptoDto.Vwap24Hr,
                    Explorer = cryptoDto.Explorer
                };
                _context.CryptoInfos.Add(cryptoInfo);
            }
            await _context.SaveChangesAsync();
        }
    }
}
