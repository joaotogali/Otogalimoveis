using Microsoft.AspNetCore.Mvc;
using Otogalimoveis.Domain.Model;
using Otogalimoveis.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otogalimoveis.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlugueisController : ControllerBase
    {
        private readonly IAluguelService _aluguelService;

        public AlugueisController(IAluguelService aluguelService)
        {
            _aluguelService = aluguelService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluguel>>> GetAll()
        {
            var alugueis = await _aluguelService.GetAllAsync();
            return Ok(alugueis);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aluguel>> GetById(int id)
        {
            var aluguel = await _aluguelService.GetByIdAsync(id);
            if (aluguel == null)
            {
                return NotFound();
            }
            return Ok(aluguel);
        }

        [HttpGet("ativos")]
        public async Task<ActionResult<IEnumerable<Aluguel>>> GetAlugueisAtivos()
        {
            var alugueis = await _aluguelService.GetAlugueisAtivosAsync();
            return Ok(alugueis);
        }

        [HttpGet("imovel/{imovelId}")]
        public async Task<ActionResult<IEnumerable<Aluguel>>> GetAlugueisPorImovel(int imovelId)
        {
            var alugueis = await _aluguelService.GetAlugueisPorImovelAsync(imovelId);
            return Ok(alugueis);
        }

        [HttpGet("locatario/{locatarioId}")]
        public async Task<ActionResult<IEnumerable<Aluguel>>> GetAlugueisPorLocatario(int locatarioId)
        {
            var alugueis = await _aluguelService.GetAlugueisPorLocatarioAsync(locatarioId);
            return Ok(alugueis);
        }

        [HttpGet("{id}/tempo")]
        public async Task<ActionResult<int>> GetTempoAluguel(int id)
        {
            try
            {
                var tempoEmDias = await _aluguelService.CalcularTempoAluguelEmDiasAsync(id);
                return Ok(tempoEmDias);
            }
            catch (System.ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Aluguel>> Create([FromBody] Aluguel aluguel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _aluguelService.AddAsync(aluguel);
                return CreatedAtAction(nameof(GetById), new { id = aluguel.Id }, aluguel);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Aluguel aluguel)
        {
            if (id != aluguel.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _aluguelService.UpdateAsync(aluguel);
                return NoContent();
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _aluguelService.DeleteAsync(id);
                return NoContent();
            }
            catch (System.ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{id}/finalizar")]
        public async Task<IActionResult> FinalizarAluguel(int id)
        {
            try
            {
                await _aluguelService.FinalizarAluguelAsync(id);
                return NoContent();
            }
            catch (System.ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (System.InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
} 