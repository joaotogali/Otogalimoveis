using Microsoft.AspNetCore.Mvc;
using Otogalimoveis.Domain.Data;
using Otogalimoveis.Domain.Model;

namespace Otogalimoveis.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlugueisController : ControllerBase
    {
        private readonly IAluguelData _aluguelData;

        public AlugueisController(IAluguelData aluguelData)
        {
            _aluguelData = aluguelData;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluguel>>> GetAll()
        {
            var alugueis = await _aluguelData.GetAllAsync();
            return Ok(alugueis);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aluguel>> GetById(int id)
        {
            var aluguel = await _aluguelData.GetByIdAsync(id);
            if (aluguel == null)
            {
                return NotFound();
            }
            return Ok(aluguel);
        }

        [HttpGet("ativos")]
        public async Task<ActionResult<IEnumerable<Aluguel>>> GetAlugueisAtivos()
        {
            var alugueis = await _aluguelData.GetAlugueisAtivosAsync();
            return Ok(alugueis);
        }

        [HttpGet("imovel/{imovelId}")]
        public async Task<ActionResult<IEnumerable<Aluguel>>> GetAlugueisPorImovel(int imovelId)
        {
            var alugueis = await _aluguelData.GetAlugueisPorImovelAsync(imovelId);
            return Ok(alugueis);
        }

        [HttpGet("locatario/{locatarioId}")]
        public async Task<ActionResult<IEnumerable<Aluguel>>> GetAlugueisPorLocatario(int locatarioId)
        {
            var alugueis = await _aluguelData.GetAlugueisPorLocatarioAsync(locatarioId);
            return Ok(alugueis);
        }

        [HttpPost]
        public async Task<ActionResult<Aluguel>> Create([FromBody] Aluguel aluguel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _aluguelData.AddAsync(aluguel);
            return CreatedAtAction(nameof(GetById), new { id = aluguel.Id }, aluguel);
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

            await _aluguelData.UpdateAsync(aluguel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _aluguelData.DeleteAsync(id);
            return NoContent();
        }
    }
} 