using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Repositories;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Application.Models.Request;
using Application.Services;
using System.Security.Claims;


namespace Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        private readonly ApplicationContext _context;

        private readonly IEventService _eventService;

        private readonly IEventOrganizerService _eventOrganizerService;
        public EventsController(ApplicationContext context, IEventService eventService, IEventOrganizerService eventOrganizerService)
        {
            _context = context;
            _eventService = eventService;
            _eventOrganizerService = eventOrganizerService;
        }



        [Authorize(Policy = "EventOrganizer")]
        [HttpPost("/create-event")]
        public IActionResult CreateEvent(EventsCreateRequest createEventRequest)
        {
            if (createEventRequest == null)
            {
                return BadRequest("Invalid event data");
            }

            if (createEventRequest.Date < DateTime.Now)
            {
                return BadRequest("Invalid date");
            }

            var claimId = _eventService.GetUserInfo(User);


            var eventOrganizer = _context.EventsOrganizers.Find(claimId);
            if (eventOrganizer == null)
            {
                return NotFound("Organizer not found");
            }

            var createEvent = _eventService.CreateEvent(createEventRequest, claimId);

            if (createEvent == null)
            {
                return BadRequest("The event that you are trying to create already exists");
            }
            return CreatedAtAction(nameof(GetEventById), new { id = claimId }, createEvent);
        }



        [Authorize(Policy = "EventOrganizer")]
        [HttpGet("organizer/events")]
        public IActionResult GetEventsByOrganizer()
        {
            var organizerId = _eventService.GetUserInfo(User);

            var events = _eventService.GetEventsByOrganizerId(organizerId);

            if (events == null)
            {
                return NotFound("No events for this organizer or you are not an organizer");
            }
            return Ok(events);
        }



        [Authorize(Policy = "EventOrganizer")]
        [HttpGet("{Id}")]
        public IActionResult GetEventById(int Id)
        {
            var eventToSearch = _eventService.GetEventById(Id);
            var organizerId = _eventService.GetUserInfo(User);

            if (eventToSearch.EventOrganizerId != organizerId)
            {
                return StatusCode(403,"This event does not belong to you");
            }
            else if (eventToSearch == null)
            {
                return NotFound("Event not found");
            }
            return Ok(eventToSearch);
        }



        [Authorize(Policy = "EventOrganizer")]
        [HttpGet("organizers/events/event/tickets/available")]
        public IActionResult CheckAvailableTickets(int eventId)
        {
            var eventOrganizerId = _eventService.GetUserInfo(User);
            

            int result = _eventOrganizerService.CheckAvailableTickets(eventOrganizerId, eventId);

            if (result == -1)
            {
                return NotFound("Event not found");
            }
            else if (result == -2)
            {
                return NotFound("Organizer not found");
            }
            else if(result == -3) 
            {
                return StatusCode(403, "It is not your event");
            }
               
                return Ok(result);
            
        }

        [Authorize(Policy = "EventOrganizer")]
        [HttpGet("organizers/events/event/tickets/sold/{eventId}")]
        public IActionResult CheckSoldTickets(int eventId)
        {
            var eventOrganizerId = _eventService.GetUserInfo(User);

            int result = _eventOrganizerService.CheckSoldTickets(eventOrganizerId, eventId);
            if (result == -1)
            {
                return NotFound("Event not found");
            }
            else if (result == -2)
            {
                return NotFound("Organizer not found");
            }
            else if (result == -3)
            {
                return StatusCode(403, "It is not your event");
            }
                return Ok(result);
        }

        [Authorize(Policy = "EventOrganizer")]
        [HttpPut("/update-event")]
        public IActionResult Update(EventUpdateRequest eventToUpdate)
        {
            var organizerId = _eventService.GetUserInfo(User);
            var updatedEvent = _eventService.UpdateEvent(eventToUpdate, organizerId);

            if (updatedEvent == null)
            {
                return NotFound("Event not found");
            }
            else if (updatedEvent.EventOrganizerId != organizerId) 
            {
                return StatusCode(403, "It is not your event");
            }
            return NoContent();
        }


        [Authorize(Policy = "EventOrganizer")]
        [HttpDelete("{eventId}")]
        public IActionResult Delete(int eventId)
        {
            var organizerId = _eventService.GetUserInfo(User);
            var deletedEvent = _eventService.DeleteEvent(eventId, organizerId);
            if (deletedEvent == 0)
            {
                return NotFound("Event not found");
            }  
            else if (deletedEvent == -1) 
            {
                return StatusCode(403, "It is not your event");              
            }
            return NoContent();                                              
        }

        [Authorize(Policy = "SuperAdmin")]
        [HttpDelete("{organizerId}/{eventId}")]
        public IActionResult Delete(int organizerId, int eventId) 
        {
            var deletedEvent = _eventService.DeleteEvent(eventId, organizerId);
            if (deletedEvent == 0)
            {
                return NotFound("Event not found");
            }
            else if (deletedEvent == -1)
            {
                return StatusCode(403, "It is not your event");
            }
            return NoContent();
        }
    }
}
