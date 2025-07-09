using Microsoft.AspNetCore.Mvc;
using Otogalimoveis.Application.Services;
using Otogalimoveis.Application.DTOs;

namespace Otogalimoveis.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImoveisDTOController : ControllerBase
    {
        private readonly IImovelServiceDTO _imovelService;

        public ImoveisDTOController(IImovelServiceDTO imovelService)
        {
            _imovelService = imovelService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImovelDTO>>> GetAll()
        {
            try
            {
                var imoveis = await _imovelService.GetAllAsync();
                return Ok(imoveis);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ImovelDetailDTO>> GetById(int id)
        {
            try
            {
                var imovel = await _imovelService.GetByIdAsync(id);
                if (imovel == null)
                {
                    return NotFound(new { message = "Imóvel não encontrado." });
                }
                return Ok(imovel);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ImovelDTO>> Create([FromBody] CreateImovelDTO createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdImovel = await _imovelService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = createdImovel.Id }, createdImovel);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateImovelDTO updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedImovel = await _imovelService.UpdateAsync(id, updateDto);
                return Ok(updatedImovel);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _imovelService.DeleteAsync(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound(new { message = "Imóvel não encontrado." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Endpoints específicos
        [HttpGet("disponiveis")]
        public async Task<ActionResult<IEnumerable<ImovelDTO>>> GetDisponiveis()
        {
            try
            {
                var imoveis = await _imovelService.GetImoveisDisponiveisAsync();
                return Ok(imoveis);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("tipo/{tipo}")]
        public async Task<ActionResult<IEnumerable<ImovelDTO>>> GetPorTipo(string tipo)
        {
            try
            {
                var imoveis = await _imovelService.GetImoveisPorTipoAsync(tipo);
                return Ok(imoveis);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("faixa-valor")]
        public async Task<ActionResult<IEnumerable<ImovelDTO>>> GetPorFaixaValor(
            [FromQuery] decimal valorMinimo, 
            [FromQuery] decimal valorMaximo)
        {
            try
            {
                var imoveis = await _imovelService.GetImoveisPorFaixaValorAsync(valorMinimo, valorMaximo);
                return Ok(imoveis);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}/disponivel")]
        public async Task<ActionResult<bool>> IsDisponivel(int id)
        {
            try
            {
                var isDisponivel = await _imovelService.IsImovelDisponivelAsync(id);
                return Ok(isDisponivel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
} 