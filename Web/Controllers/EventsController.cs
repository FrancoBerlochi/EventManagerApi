﻿using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Repositories;
using Infrastructure;


namespace Web.Controllers
{
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

        [HttpGet("organizer/{organizerId}/getEventOrganizer")]
        public IActionResult GetEventOrganizer(int organizerId)
        {
            var organizer = _eventOrganizerService.GetEventOrganizer(organizerId);
            if (organizer == null)
            {
                return NotFound("No organizer found");
            }
            return Ok(organizer);
        }

        [HttpGet("organizer/{organizerId}/events")]
        public IActionResult GetEventsByOrganizer(int organizerId)
        {
            var events = _eventService.GetEventsByOrganizerId(organizerId);
            if (events == null || !events.Any())
            {
                return NotFound("No events for this organizer");
            }
            return Ok(events);
        }

        [HttpPost("organizer/{organizerId}/events")]

        public IActionResult CreateEvent(int organizerId, [FromQuery]EventsDto createEventDto)

        {
            if (createEventDto == null)
            {
                return BadRequest("Invalid event data");
            }

            var eventOrganizer = _context.EventsOrganizers.Find(organizerId);
            if (eventOrganizer == null)
            {
                return NotFound("Organizer not found.");
            }


            _eventService.CreateEvent(
                createEventDto.Name,
                createEventDto.Address,
                createEventDto.City,
                createEventDto.Date,
                createEventDto.NumberOfTickets,
                createEventDto.Category,
                createEventDto.Price,
                createEventDto.EventOrganizerId
            );

            return Ok("Event created successfully.");
        }

    }
}
