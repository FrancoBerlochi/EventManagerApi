using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEventOrganizerRepository
    {
        EventOrganizer Add(EventOrganizer eventOrganizer);
        EventOrganizer GetEventOrganizer(int eventOrganizerId);
        List<EventOrganizer> GetAll();
        void Update(int id, EventOrganizer eventOrganizer);
        void Delete(int eventOrganizerId);
        int CheckAvailableTickets(int eventOrganizerId, int eventId);
        int CheckSoldTickets(int eventOrganizerId, int eventId);
    }
}
