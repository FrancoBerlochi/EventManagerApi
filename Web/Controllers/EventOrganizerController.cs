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
        [HttpPost("CreateEventOrganizer")]
        public IActionResult Create(EventOrganizerCreateRequest eventOrganizerCreateRequest)
        {
            if (eventOrganizerCreateRequest == null)
            {
                return BadRequest("Invalid organizer data");
            }
            var organizerCreated = _eventOrganizerService.Add(eventOrganizerCreateRequest);
            
            return CreatedAtAction(nameof(GetEventOrganizer), new {id = organizerCreated.Id}, organizerCreated);

        }

        [Authorize(Policy = "SuperAdmin")]
        [HttpGet("organizer")]
        public IActionResult GetEventOrganizer(int organizerId)
        {
            var organizer = _eventOrganizerService.GetEventOrganizer(organizerId);
            if (organizer == null)
            {
                return NotFound("Event organizer not found");
            }
            return Ok(organizer);                            
        }


        [Authorize(Policy = "SuperAdmin")]
        [HttpGet("GetAllOrganizers")]
        public IActionResult GetAll()
        {   
             var organizers = _eventOrganizerService.GetAll();
            if (organizers == null)
            {
                return NotFound("Without events organizers");
            }
            return Ok(organizers);
        }



        [Authorize(Policy = "SuperAdmin")]
        [HttpPut("UpdateOrganizer")]
        public IActionResult Update(int id, [FromQuery] EventOrganizerUpdateRequest eventOrganizerUpdateRequest)
        {
            var organizerToUpdate = _context.Users.OfType<EventOrganizer>().FirstOrDefault(e => e.Id == id);
            if (organizerToUpdate == null)
            {
                return NotFound("Organizer not found");
            }
            _eventOrganizerService.Update(id, eventOrganizerUpdateRequest);
           return NoContent();   
            
            
        }


        [Authorize(Policy = "SuperAdmin")]
        [HttpDelete()]
        public IActionResult Delete(int id)
        {
            var organizerToDelete = _context.Users.OfType<EventOrganizer>().FirstOrDefault(e => e.Id == id);
            if (organizerToDelete == null) 
            {
                return NotFound("Organizer not found");
            }
            _eventOrganizerService.Delete(id);
                return NoContent();
            
        }
    }
}
