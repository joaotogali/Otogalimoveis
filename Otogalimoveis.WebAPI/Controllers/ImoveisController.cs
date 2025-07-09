using Microsoft.AspNetCore.Mvc;
using Otogalimoveis.Domain.Model;
using Otogalimoveis.Domain.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otogalimoveis.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImoveisController : ControllerBase
    {
        private readonly IImovelData _imovelData;

        public ImoveisController(IImovelData imovelData)
        {
            _imovelData = imovelData;
        }

        // GET: api/imoveis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Imovel>>> GetAll()
        {
            var imoveis = await _imovelData.GetAllAsync();
            return Ok(imoveis);
        }

        // GET: api/imoveis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Imovel>> GetById(int id)
        {
            var imovel = await _imovelData.GetByIdAsync(id);
            if (imovel == null)
            {
                return NotFound();
            }
            return Ok(imovel);
        }

        // POST: api/imoveis
        [HttpPost]
        public async Task<ActionResult<Imovel>> Create([FromBody] Imovel imovel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _imovelData.AddAsync(imovel);
            return CreatedAtAction(nameof(GetById), new { id = imovel.Id }, imovel);
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

            await _imovelData.UpdateAsync(imovel);
            return NoContent();
        }

        // DELETE: api/imoveis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _imovelData.DeleteAsync(id);
            return NoContent();
        }
    }
} 