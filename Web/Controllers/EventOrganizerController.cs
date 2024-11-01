using Application.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Models.Request;
using Domain.Entities;

namespace Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EventOrganizerController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IEventOrganizerService _eventOrganizerService;
        public EventOrganizerController(ApplicationContext context, IEventOrganizerService eventOrganizerService)
        {
            _context = context;
            _eventOrganizerService = eventOrganizerService;
        }

        [Authorize(Policy = "SuperAdmin")]
        [HttpPost()]
        public IActionResult Create([FromQuery] EventOrganizerCreateRequest eventOrganizerCreateRequest)
        {
            if (eventOrganizerCreateRequest == null)
            {
                return BadRequest("Invalid organizer data");
            }
            var organizerCreated = _eventOrganizerService.Add(eventOrganizerCreateRequest);
            if(organizerCreated != null)
            {
                return Ok(organizerCreated);
            }

            return BadRequest("error");
        }

        [Authorize(Policy = "SuperAdmin")]
        [HttpDelete()]
        public IActionResult Delete(int id)
        {
            var organizerToDelete = _context.Users.OfType<EventOrganizer>().FirstOrDefault(e => e.Id == id);
            if(organizerToDelete != null)
            {
                _eventOrganizerService.Delete(id);
                return Ok("Deleted");
            }
            return BadRequest("error");
        }
    }
}
