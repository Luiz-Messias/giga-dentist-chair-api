using Microsoft.AspNetCore.Mvc;
using projeto_giga.Application.DTOs;
using projeto_giga.Application.Interfaces;

namespace projeto_giga.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AllocationController : ControllerBase
    {
        private readonly IAllocationService allocationService;

        public AllocationController(IAllocationService allocationService)
        {
            this.allocationService = allocationService;
        }

        /// <summary>
        /// Aloca automaticamente cadeiras odontológicas proporcionalmente com base nas regras.
        /// </summary>
        /// <param name="request">Regras de alocação.</param>
        [HttpPost("allocate")]
        public IActionResult AllocateChair([FromBody] AllocateRequestDto request)
        {
            var response = allocationService.AllocateChairsAutomatically(request);
            return response.Success
                ? Ok(response)
                : BadRequest(response);
        }

        /// <summary>
        /// Retorna a lista de todas as alocações realizadas.
        /// </summary>
        [HttpGet("allocations")]
        public IActionResult GetAllocations()
        {
            var response = allocationService.GetAllAllocations();
            return response.Success
                ? Ok(response)
                : StatusCode(500, response);
        }
    }
}