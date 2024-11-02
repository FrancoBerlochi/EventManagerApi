using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.DTO;
using Application.Models.Request;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IEventService
    {

        EventsDto CreateEvent(EventsCreateRequest eventsRequest);
        EventsDto GetEventById(int eventId);
        List<EventsDto> GetAllEvents();
        List<EventsDto> GetEventsByOrganizerId(int organizerId);
        void UpdateEvent(EventUpdateRequest eventToUpdate);
        void DeleteEvent(int eventId);
    }
}
