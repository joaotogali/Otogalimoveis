using Microsoft.AspNetCore.Mvc;
using Otogalimoveis.Domain.Model;
using Otogalimoveis.Domain.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otogalimoveis.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocatariosController : ControllerBase
    {
        private readonly ILocatarioData _locatarioData;

        public LocatariosController(ILocatarioData locatarioData)
        {
            _locatarioData = locatarioData;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Locatario>>> GetAll()
        {
            var locatarios = await _locatarioData.GetAllAsync();
            return Ok(locatarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Locatario>> GetById(int id)
        {
            var locatario = await _locatarioData.GetByIdAsync(id);
            if (locatario == null)
            {
                return NotFound();
            }
            return Ok(locatario);
        }

        [HttpPost]
        public async Task<ActionResult<Locatario>> Create([FromBody] Locatario locatario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _locatarioData.AddAsync(locatario);
            return CreatedAtAction(nameof(GetById), new { id = locatario.Id }, locatario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Locatario locatario)
        {
            if (id != locatario.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _locatarioData.UpdateAsync(locatario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _locatarioData.DeleteAsync(id);
            return NoContent();
        }
    }
} 