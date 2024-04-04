using InsidersTestTask.DTO;
using InsidersTestTask.Interfaces;
using InsidersTestTask.Models;
using InsidersTestTask.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InsidersTestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptosController : ControllerBase
    {
        private readonly CryptoService _cryptoService;
        private readonly ICryptoRepository _cryptoRepository;

        public CryptosController(CryptoService cryptoService, ICryptoRepository cryptoRepository)
        {
            _cryptoService = cryptoService;
            _cryptoRepository = cryptoRepository;
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

        // GET: /cryptos/db
        [HttpGet("db")]
        public async Task<IActionResult> GetAllCryptosFromDb()
        {
            var cryptos = await _cryptoRepository.GetAllCryptosAsync();
            return Ok(cryptos);
        }

        // DELETE: /cryptos/db/{id}
        [HttpDelete("db/{id}")]
        public async Task<IActionResult> DeleteCrypto(string id)
        {
            await _cryptoRepository.DeleteCryptoAsync(id);
            return NoContent();
        }

        [HttpGet("db/{id}")]
        public async Task<IActionResult> GetCrypto(string id)
        {
            var cryptoFromDb = await _cryptoRepository.GetCryptoByIdAsync(id);
            if (cryptoFromDb == null)
            {
                return NotFound();
            }

            return Ok(cryptoFromDb);
        }

        [HttpPatch("db/{id}")]
        public async Task<IActionResult> UpdateCrypto(string id, [FromBody] CryptoDTO updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest();
            }

            var cryptoFromDb = await _cryptoRepository.GetCryptoByIdAsync(id);
            if (cryptoFromDb == null)
            {
                return NotFound();
            }

            cryptoFromDb.Rank = updateDto.Rank;
            cryptoFromDb.Symbol = updateDto.Symbol;
            cryptoFromDb.Name = updateDto.Name;
            cryptoFromDb.Supply = updateDto.Supply;
            if (updateDto.MaxSupply.HasValue) cryptoFromDb.MaxSupply = updateDto.MaxSupply.Value;
            cryptoFromDb.MarketCapUsd = updateDto.MarketCapUsd;
            cryptoFromDb.VolumeUsd24Hr = updateDto.VolumeUsd24Hr;
            cryptoFromDb.PriceUsd = updateDto.PriceUsd;
            cryptoFromDb.ChangePercent24Hr = updateDto.ChangePercent24Hr;
            cryptoFromDb.Vwap24Hr = updateDto.Vwap24Hr;
            cryptoFromDb.Explorer = updateDto.Explorer;

            var updateResult = await _cryptoRepository.UpdateCryptoAsync(id, cryptoFromDb);

            if (!updateResult)
            {
                return BadRequest("Update failed");
            }

            return NoContent();
        }
    }
}
