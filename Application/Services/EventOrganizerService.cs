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

namespace Application.Services
{
    public class EventOrganizerService : IEventOrganizerService
    {
        private readonly IEventOrganizerRepository _eventOrganizerRepository;

        public EventOrganizerService(IEventOrganizerRepository eventOrganizerRepository)
        {
            _eventOrganizerRepository = eventOrganizerRepository;
        }

        public EventOrganizer GetEventOrganizer(int eventOrganizerId) 
        {
            return _eventOrganizerRepository.GetEventOrganizer(eventOrganizerId);
        }

        public int CheckAvailableTickets(int eventOrganizerId, int eventId)
        {
            return _eventOrganizerRepository.CheckAvailableTickets(eventOrganizerId, eventId);
        }

        public int CheckSoldTickets(int eventOrganizerId, int eventId)
        {
            return _eventOrganizerRepository.CheckSoldTickets(eventOrganizerId, eventId);
        }

        public EventOrganizerDto Add(EventOrganizerCreateRequest eventOrganizerCreateRequest)
        {
            var newOrganizer = new EventOrganizer(eventOrganizerCreateRequest.Name, eventOrganizerCreateRequest.Email, eventOrganizerCreateRequest.Password, eventOrganizerCreateRequest.Phone);
            var createdOrganizer = _eventOrganizerRepository.Add(newOrganizer);
            return EventOrganizerDto.Create(createdOrganizer);
        }
        public void Update(EventOrganizerUpdateRequest eventOrganizerUpdateRequest)
        {
            var organizerToUpdate = new EventOrganizer(eventOrganizerUpdateRequest.Name, eventOrganizerUpdateRequest.Email, eventOrganizerUpdateRequest.Password, eventOrganizerUpdateRequest.Phone);
            _eventOrganizerRepository.Update(organizerToUpdate);
        }

        public void Delete(int eventOrganizerId)
        {
            _eventOrganizerRepository.Delete(eventOrganizerId);
        }
    }
}
