using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTO
{
    public class EventOrganizerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; } = "EventOrganizer";

        public static EventOrganizerDto Create(EventOrganizer eventOrganizerToCreate)
        {
            EventOrganizerDto dto = new EventOrganizerDto();
            dto.Id = eventOrganizerToCreate.Id;
            dto.Name = eventOrganizerToCreate.Name;
            dto.Email = eventOrganizerToCreate.Email;
            dto.Password = eventOrganizerToCreate.Password;
            dto.Phone = eventOrganizerToCreate.Phone;
            dto.Role = "EventOrganizer";

            return dto;
        }
    }
}
