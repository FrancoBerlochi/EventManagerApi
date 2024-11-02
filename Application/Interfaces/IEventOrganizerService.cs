using Application.Models.DTO;
using Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEventOrganizerService
    {
        EventOrganizerDto Add(EventOrganizerCreateRequest eventOrganizerCreateRequest);
        EventOrganizerDto GetEventOrganizer(int eventOrganizerId);
        List<EventOrganizerDto> GetAll();
        void Update(int id, EventOrganizerUpdateRequest eventOrganizerUpdateRequest);
        void Delete(int eventOrganizerId);
        int CheckAvailableTickets(int eventOrganizerId, int eventId);
        int CheckSoldTickets(int eventOrganizerId, int eventId);
    }
}
