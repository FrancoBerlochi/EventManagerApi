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
using Domain.Exceptions;

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
                throw new NotFoundException($"No organizer found with ID: {eventOrganizerId}");
            }
            return EventOrganizerDto.Create(eventOrganizer);
        }

        public List<EventOrganizerDto> GetAll()
        {
            var allOrganizers = _eventOrganizerRepository.GetAll();
            var organizersDto = new List<EventOrganizerDto>();

            foreach(var organizer in allOrganizers)
            {
                organizersDto.Add(EventOrganizerDto.Create(organizer));
            }
            if(organizersDto.Count <= 0)
            {
                throw new Exception("No organizers found");
            }
            return organizersDto;
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
        public void Update(int id, EventOrganizerUpdateRequest eventOrganizerUpdateRequest)
        {
            var organizerToUpdate = new EventOrganizer(eventOrganizerUpdateRequest.Name, eventOrganizerUpdateRequest.Email, eventOrganizerUpdateRequest.Password, eventOrganizerUpdateRequest.Phone);
            _eventOrganizerRepository.Update(id, organizerToUpdate);
        }

        public void Delete(int eventOrganizerId)
        {
            _eventOrganizerRepository.Delete(eventOrganizerId);
        }
    }
}
