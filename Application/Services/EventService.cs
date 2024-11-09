using Application.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Models.Request;
using Application.Models.DTO;


namespace Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public EventsDto CreateEvent(EventsCreateRequest eventRequest, int id) 
        { 
            var newEvent = new Event(eventRequest.Name, eventRequest.Address, eventRequest.City, eventRequest.Date, eventRequest.NumberOfTickets, eventRequest.Category, eventRequest.Price, id);
            var createdEvent = _eventRepository.Add(newEvent, id);
            return EventsDto.Create(createdEvent);
        }

        public EventsDto GetEventById(int eventId)
        {
            var eventToGet = _eventRepository.GetById(eventId);
            if (eventToGet == null)
            {
                return null;
            }
            return EventsDto.Create(eventToGet);
        }

        public List<EventsDto> GetAllEvents() 
        { 
            var events = _eventRepository.GetAll();
            var eventsDto = new List<EventsDto>();
            foreach (var e in events)
            {
                eventsDto.Add(EventsDto.Create(e));
            }
            return eventsDto;
        }

        public List<EventsDto> GetEventsByOrganizerId(int organizerId)
        {
            var eventsByOrg = _eventRepository.GetEventsByOrganizerId(organizerId).ToList();
            var eventsDto = new List<EventsDto>();
            foreach(var e in eventsByOrg)
            {
                eventsDto.Add(EventsDto.Create(e));
            }
            return eventsDto;
        }

        public EventsDto UpdateEvent(EventUpdateRequest eventUpdateRequest, int organizerId)
        {
            var eventToUpdate = new Event(eventUpdateRequest.Id, eventUpdateRequest.Name, eventUpdateRequest.Address, eventUpdateRequest.City, eventUpdateRequest.Date, eventUpdateRequest.Category, eventUpdateRequest.Price);
            var eventUpdated = _eventRepository.Update(eventToUpdate, organizerId);
            if (eventUpdated == null) 
            {
                return null;
            }
            return EventsDto.Create(eventUpdated);
        }

        public int DeleteEvent(int eventId, int organizerId)
        {
           return _eventRepository.Delete(eventId, organizerId);
        }
    }
}
