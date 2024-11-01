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

        public EventsDto CreateEvent(EventsRequest eventRequest) 
        { 
            var newEvent = new Event(eventRequest.Name, eventRequest.Address, eventRequest.City, eventRequest.Date, eventRequest.NumberOfTickets, eventRequest.Category, eventRequest.Price, eventRequest.EventOrganizerId);
            var createdEvent = _eventRepository.Add(newEvent, eventRequest.EventOrganizerId);
            return EventsDto.Create(createdEvent);
        }

        public EventsDto GetEventById(int eventId)
        {
            var eventToGet = _eventRepository.GetById(eventId);
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

        public void UpdateEvent(EventUpdateRequest eventUpdateRequest)
        {
            var eventToUpdate = new Event(eventUpdateRequest.Id, eventUpdateRequest.Name, eventUpdateRequest.Address, eventUpdateRequest.City, eventUpdateRequest.Date, eventUpdateRequest.Category, eventUpdateRequest.Price);
            _eventRepository.Update(eventToUpdate);
        }

        public void DeleteEvent(int eventId)
        {
            _eventRepository.Delete(eventId);
        }
    }
}
