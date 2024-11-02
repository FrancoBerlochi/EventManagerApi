using Application.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;

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
        public IActionResult Create([FromBody] EventOrganizerCreateRequest eventOrganizerCreateRequest)
        {
            if (eventOrganizerCreateRequest == null)
            {
                return BadRequest("Invalid organizer data");
            }
            var organizerCreated = _eventOrganizerService.Add(eventOrganizerCreateRequest);
            if(organizerCreated != null)
            {
                return CreatedAtAction(nameof(GetEventOrganizer), new {id = organizerCreated.Id}, organizerCreated);
            }

            return BadRequest("Error creating the organizer");
        }




        [Authorize(Policy = "SuperAdmin")]
        [HttpGet("organizer/{organizerId}")]
        public IActionResult GetEventOrganizer(int organizerId)
        {
            try
            {
                var organizer = _eventOrganizerService.GetEventOrganizer(organizerId);
                return Ok(organizer);
            }
            catch (NotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
        }


        [Authorize(Policy = "SuperAdmin")]
        [HttpGet()]
        public IActionResult GetAll()
        {
            try
            {
                var organizers = _eventOrganizerService.GetAll();
                return Ok(organizers);
            } catch (NotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
        }



        [Authorize(Policy = "SuperAdmin")]
        [HttpPut()]
        public IActionResult Update(int id, [FromQuery] EventOrganizerUpdateRequest eventOrganizerUpdateRequest)
        {
            var organizerToUpdate = _context.Users.OfType<EventOrganizer>().FirstOrDefault(e => e.Id == id);
            if (organizerToUpdate != null)
            {
                _eventOrganizerService.Update(id, eventOrganizerUpdateRequest);
                return Ok("Updated");
            }
            return BadRequest("error");
        }


        [Authorize(Policy = "SuperAdmin")]
        [HttpDelete()]
        public IActionResult Delete(int id)
        {
            try
            {
                _eventOrganizerService.Delete(id);
                return Ok("Deleted");
            } catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
