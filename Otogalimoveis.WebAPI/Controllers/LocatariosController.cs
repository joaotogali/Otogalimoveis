using Microsoft.AspNetCore.Mvc;
using Otogalimoveis.Domain.Model;
using Otogalimoveis.Application.Services;

namespace Otogalimoveis.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocatariosController : ControllerBase
    {
        private readonly ILocatarioService _locatarioService;

        public LocatariosController(ILocatarioService locatarioService)
        {
            _locatarioService = locatarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Locatario>>> GetAll()
        {
            try
            {
                var locatarios = await _locatarioService.GetAllAsync();
                return Ok(locatarios);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Locatario>> GetById(int id)
        {
            try
            {
                var locatario = await _locatarioService.GetByIdAsync(id);
                if (locatario == null)
                {
                    return NotFound(new { message = "Locatário não encontrado." });
                }
                return Ok(locatario);
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
        public async Task<ActionResult<Locatario>> Create([FromBody] Locatario locatario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdLocatario = await _locatarioService.CreateAsync(locatario);
                return CreatedAtAction(nameof(GetById), new { id = createdLocatario.Id }, createdLocatario);
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
        public async Task<IActionResult> Update(int id, [FromBody] Locatario locatario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedLocatario = await _locatarioService.UpdateAsync(id, locatario);
                return Ok(updatedLocatario);
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
                var result = await _locatarioService.DeleteAsync(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound(new { message = "Locatário não encontrado." });
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
        [HttpGet("cpf/{cpf}")]
        public async Task<ActionResult<IEnumerable<Locatario>>> GetByCpf(string cpf)
        {
            try
            {
                var locatarios = await _locatarioService.GetByCpfAsync(cpf);
                return Ok(locatarios);
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

        [HttpGet("email/{email}")]
        public async Task<ActionResult<IEnumerable<Locatario>>> GetByEmail(string email)
        {
            try
            {
                var locatarios = await _locatarioService.GetByEmailAsync(email);
                return Ok(locatarios);
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

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Locatario>>> GetActiveLocatarios()
        {
            try
            {
                var locatarios = await _locatarioService.GetActiveLocatariosAsync();
                return Ok(locatarios);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}/rentals")]
        public async Task<ActionResult<IEnumerable<Aluguel>>> GetRentalsByLocatario(int id)
        {
            try
            {
                var rentals = await _locatarioService.GetRentalsByLocatarioAsync(id);
                return Ok(rentals);
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

        [HttpGet("{id}/has-active-rentals")]
        public async Task<ActionResult<bool>> HasActiveRentals(int id)
        {
            try
            {
                var hasActiveRentals = await _locatarioService.HasActiveRentalsAsync(id);
                return Ok(hasActiveRentals);
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
    }
} 