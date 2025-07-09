using Microsoft.AspNetCore.Mvc;
using Otogalimoveis.Domain.Model;
using Otogalimoveis.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otogalimoveis.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImoveisController : ControllerBase
    {
        private readonly IImovelService _imovelService;

        public ImoveisController(IImovelService imovelService)
        {
            _imovelService = imovelService;
        }

        // GET: api/imoveis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Imovel>>> GetAll()
        {
            var imoveis = await _imovelService.GetAllAsync();
            return Ok(imoveis);
        }

        // GET: api/imoveis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Imovel>> GetById(int id)
        {
            var imovel = await _imovelService.GetByIdAsync(id);
            if (imovel == null)
            {
                return NotFound();
            }
            return Ok(imovel);
        }

        // GET: api/imoveis/disponiveis
        [HttpGet("disponiveis")]
        public async Task<ActionResult<IEnumerable<Imovel>>> GetDisponiveis()
        {
            var imoveis = await _imovelService.GetImoveisDisponiveisAsync();
            return Ok(imoveis);
        }

        // GET: api/imoveis/tipo/{tipo}
        [HttpGet("tipo/{tipo}")]
        public async Task<ActionResult<IEnumerable<Imovel>>> GetPorTipo(string tipo)
        {
            var imoveis = await _imovelService.GetImoveisPorTipoAsync(tipo);
            return Ok(imoveis);
        }

        // GET: api/imoveis/faixa-valor
        [HttpGet("faixa-valor")]
        public async Task<ActionResult<IEnumerable<Imovel>>> GetPorFaixaValor([FromQuery] decimal valorMinimo, [FromQuery] decimal valorMaximo)
        {
            var imoveis = await _imovelService.GetImoveisPorFaixaValorAsync(valorMinimo, valorMaximo);
            return Ok(imoveis);
        }

        // POST: api/imoveis
        [HttpPost]
        public async Task<ActionResult<Imovel>> Create([FromBody] Imovel imovel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _imovelService.AddAsync(imovel);
                return CreatedAtAction(nameof(GetById), new { id = imovel.Id }, imovel);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/imoveis/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Imovel imovel)
        {
            if (id != imovel.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _imovelService.UpdateAsync(imovel);
                return NoContent();
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/imoveis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _imovelService.DeleteAsync(id);
                return NoContent();
            }
            catch (System.InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
} 