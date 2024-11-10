using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Models.DTO;
using Application.Models.Request;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IEventService
    {
        int GetUserInfo(ClaimsPrincipal User);
        EventsDto CreateEvent(EventsCreateRequest eventsRequest, int id);
        EventsDto GetEventById(int eventId);
        List<EventsDto> GetAllEvents();
        List<EventsDto> GetEventsByOrganizerId(int organizerId);
        EventsDto UpdateEvent(EventUpdateRequest eventToUpdate, int organizerId);
        int DeleteEvent(int eventId, int organizerId);
    }
}
