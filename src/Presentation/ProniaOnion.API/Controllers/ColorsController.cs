using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstraction.Services;
using ProniaOnion.Application.DTOs.Color;

namespace ProniaOnion.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _service;

        public ColorsController(IColorService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> Get(int page, int take)
        {
            return Ok(await _service.GetAllPaginated(page, take));
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
        //    return StatusCode(StatusCodes.Status200OK, await _service.GetByIdAsync(id));
        //}
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    return Ok(await _service.GetByIdAsync(id));
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ColorCreateDto ColorCreateDto)
        {
            await _service.CreateAsync(ColorCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ColorUpdateDto ColorUpdateDto)
        {
            if (id <= 0) return BadRequest();
            await _service.UpdateAsync(id, ColorUpdateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            await _service.SoftDeleteAsync(id);
            return NoContent();
        }
        [HttpDelete("SoftDelete/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            await _service.SoftDeleteAsync(id);
            return NoContent();
        }
    }
}
