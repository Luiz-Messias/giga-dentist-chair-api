using Microsoft.AspNetCore.Mvc;
using projeto_giga.Application.Common;
using projeto_giga.Application.DTOs;
using projeto_giga.Application.Interfaces;

namespace projeto_giga.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DentistChairsController : ControllerBase
    {
        private readonly IDentistChairService _dentistChairService;

        public DentistChairsController(IDentistChairService dentistChairService)
        {
            _dentistChairService = dentistChairService;
        }

        /// <summary>
        /// Retorna todas as cadeiras odontológicas cadastradas.
        /// </summary>
        [HttpGet("retrieve/all")]
        public IActionResult GetAll()
        {
            var response = _dentistChairService.GetAll();
            return response.Success
                ? Ok(response)
                : StatusCode(500, response);
        }

        /// <summary>
        /// Retorna uma cadeira odontológica específica pelo ID.
        /// </summary>
        /// <param name="id">ID da cadeira odontológica.</param>
        [HttpGet("retrieve/byid/{id:int}")]
        public IActionResult GetById(int id)
        {
            var response = _dentistChairService.GetDentistChairById(id);
            return response.Success
                ? Ok(response)
                : NotFound(response);
        }

        /// <summary>
        /// Cria uma nova cadeira odontológica.
        /// </summary>
        /// <param name="dto">Dados da cadeira a ser criada.</param>
        [HttpPost("create")]
        public IActionResult Create([FromBody] DentistChairCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.Fail("Dados inválidos."));

            var response = _dentistChairService.Create(dto);
            return response.Success
                ? CreatedAtAction(nameof(GetById), new { id = dto.Number }, response)
                : BadRequest(response);
        }

        /// <summary>
        /// Atualiza os dados de uma cadeira odontológica existente.
        /// </summary>
        /// <param name="id">ID da cadeira a ser atualizada.</param>
        /// <param name="dto">Dados atualizados.</param>
        [HttpPut("update/chairdentistbyid/{id:int}")]
        public IActionResult UpdateDentistChairById(int id, [FromBody] DentistChairUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.Fail("Dados inválidos."));

            var response = _dentistChairService.Update(id, dto);
            return response.Success
                ? Ok(response)
                : NotFound(response);
        }

        /// <summary>
        /// Remove uma cadeira odontológica pelo ID.
        /// </summary>
        /// <param name="id">ID da cadeira a ser removida.</param>
        [HttpDelete("delete/{id:int}")]
        public IActionResult DeleteChairDentist(int id)
        {
            var response = _dentistChairService.Delete(id);
            return response.Success
                ? Ok(response)
                : NotFound(response);
        }
    }
}