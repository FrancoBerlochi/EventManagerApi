using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTO
{
    public class TicketDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int? ClientId { get; set; }
        public Client? Client { get; set; }
        public float Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public TicketState State { get; set; }
        public string? EventName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public DateTime? Date { get; set; }

        public static TicketDto create(Ticket ticketToShow) 
        { 
            TicketDto dto = new TicketDto();
            dto.Id = ticketToShow.Id;
            dto.Event = ticketToShow.Event;
            dto.ClientId = ticketToShow.ClientId;
            dto.Client = ticketToShow.Client;
            dto.Amount = ticketToShow.Amount;
            dto.PaymentMethod = ticketToShow.PaymentMethod;
            dto.State = ticketToShow.State;
            dto.EventName = ticketToShow.EventName;
            dto.Address = ticketToShow.Address;
            dto.City = ticketToShow.City;
            dto.Date = ticketToShow.Date;
            return dto;
        }
    }
}
