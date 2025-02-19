using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EventId { get; set; } 
        public Event Event { get; set; }
        public int? ClientId { get; set; }
        public Client? Client { get; set; }
        public float Amount { get; set; }
        public string? PaymentMethod { get; set; } 
        public TicketState State {  get; set; }
        public string? EventName { get; set; }
        public string? Address {  get; set; }
        public string? City { get; set; }
        public DateTime Date { get; set; }

        public Ticket(float amount, int eventId, TicketState state, string? paymentMethod, string eventName, string address, string city, DateTime date)
        {
            Amount = amount; 
            EventId = eventId;
            State = state;
            PaymentMethod = null;
            ClientId = null;
            EventName = eventName;
            Address = address;
            City = city;
            Date = date;
        }

        
    }
}
