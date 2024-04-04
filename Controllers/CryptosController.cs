using InsidersTestTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsidersTestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptosController : ControllerBase
    {
        private readonly CryptoService _cryptoService;

        public CryptosController(CryptoService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        // GET: /cryptos
        [HttpGet]
        public async Task<IActionResult> GetCryptos()
        {
            var cryptos = await _cryptoService.GetCryptosFromApiAsync();
            return Ok(cryptos);
        }

        // POST: /cryptos/save
        [HttpPost("save/{id}")]
        public async Task<IActionResult> SaveCrypto(string id)
        {
            var cryptoDto = await _cryptoService.GetCryptoByIdFromApiAsync(id);
            if (cryptoDto == null)
            {
                return NotFound();
            }

            await _cryptoService.SaveCryptoInfoAsync(cryptoDto);
            return Ok();
        }
    }
}
