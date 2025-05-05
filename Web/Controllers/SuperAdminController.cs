using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuperAdminController : Controller
    {
        private readonly ISuperAdminServices _superAdminService;
        private readonly IClientService _clientService;

        public SuperAdminController(ISuperAdminServices superAdminService, IClientService clientService)
        {
            _superAdminService = superAdminService;
            _clientService = clientService;
        }

        [Authorize(Policy = "SuperAdmin")]
        [HttpGet("superAdmin/update")]
        public IActionResult Update(SuperAdminUpdateRequest updateRequest)
        {
            var id = _clientService.GetUserInfo(User);

            if (id == null) 
            {
                return BadRequest();
            }

            var update = _superAdminService.Update(id, updateRequest);
            if (update)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [Authorize(Policy = "SuperAdmin")]
        [HttpGet("superAdmin/get-all-clients")]

        public IActionResult GetAllClients()
        {
            var clients = _clientService.GetAllClients();
            if (clients == null) return NotFound("No clients");
            return Ok(clients);
        }
    }
}
