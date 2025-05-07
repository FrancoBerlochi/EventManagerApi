using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Application.Models.DTO;
using Application.Models.Request;
using Domain.Enums;

namespace Application.Services
{
    public class EventOrganizerService : IEventOrganizerService
    {
        private readonly IEventOrganizerRepository _eventOrganizerRepository;

        public EventOrganizerService(IEventOrganizerRepository eventOrganizerRepository)
        {
            _eventOrganizerRepository = eventOrganizerRepository;
        }

        public EventOrganizerDto GetEventOrganizer(int eventOrganizerId) 
        {
            var eventOrganizer = _eventOrganizerRepository.GetEventOrganizer(eventOrganizerId);
            if(eventOrganizer == null)
            {
                return null;
            }
            return EventOrganizerDto.Create(eventOrganizer);
        }

        public List<EventOrganizerDto> GetAll()
        {
            var allOrganizers = _eventOrganizerRepository.GetAll();
            var organizersDto = new List<EventOrganizerDto>();

            foreach(var organizer in allOrganizers)
            {
                if (organizer == null) 
                {
                    return null;
                }
                organizersDto.Add(EventOrganizerDto.Create(organizer));
            }
            return organizersDto;
        }

        public int CheckAvailableTickets(int eventOrganizerId, int eventId)
        {

            return _eventOrganizerRepository.CheckAvailableTickets(eventOrganizerId, eventId);
        }

        public List<Object> CheckAvailableAllTickets(int eventOrganizerId) {
            var events =  _eventOrganizerRepository.CheckAvailableAllTickets(eventOrganizerId);
            var allAvailableEvents = new List<Object>();
            if(events != null) 
            {
                for (int i = 0; i < events.Count; i++)
                {
                    var countTickets = events[i].Tickets.Count(t => t.State == TicketState.Available);
                    allAvailableEvents.Add(new { events[i].Id, availableTickets = countTickets });

                }
                return allAvailableEvents;
            }
            return null;
        }

        public int CheckSoldTickets(int eventOrganizerId, int eventId)
        {
            return _eventOrganizerRepository.CheckSoldTickets(eventOrganizerId, eventId);
        }

        public int? CheckSoldAllTickets(int eventOrganizerId) 
        {
            var events = _eventOrganizerRepository.CheckSoldAllTickets(eventOrganizerId);
            int allSoldTickets = 0;
            if (events != null)
            {
               for (int i = 0;i < events.Count; i++) 
                {
                var countTickets = events[i].Tickets.Count(t => t.State == TicketState.Sold);
                allSoldTickets += countTickets;
                } 
                return allSoldTickets;
            }
            return null;
            
        }

        public EventOrganizerDto Add(EventOrganizerCreateRequest eventOrganizerCreateRequest)
        {
            var newOrganizer = new EventOrganizer(eventOrganizerCreateRequest.Name, eventOrganizerCreateRequest.Email, eventOrganizerCreateRequest.Password, eventOrganizerCreateRequest.Phone);
            var createdOrganizer = _eventOrganizerRepository.Add(newOrganizer);
            return EventOrganizerDto.Create(createdOrganizer);
        }
        public void Update(int id, EventOrganizerUpdateRequest eventOrganizerUpdateRequest)
        {
            var organizerToUpdate = new EventOrganizer(eventOrganizerUpdateRequest.Name, eventOrganizerUpdateRequest.Email, eventOrganizerUpdateRequest.Password, eventOrganizerUpdateRequest.Phone);
             _eventOrganizerRepository.Update(id, organizerToUpdate);
            
        }

        public void Delete(int eventOrganizerId)
        {
            var eventOrganizer = _eventOrganizerRepository.GetEventOrganizer(eventOrganizerId);
            if(eventOrganizer == null)
            {
                
            }
            _eventOrganizerRepository.Delete(eventOrganizerId);
        }
    }
}
