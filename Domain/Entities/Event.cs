using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfTickets { get; set; }
        public string? Category { get; set; }
        public float Price { get; set; }
        public List<Ticket> Tickets { get; set; }
        public int EventOrganizerId { get; set; }
        public EventOrganizer EventOrganizer { get; set; }


        public Event(string name, string address, string city, DateTime date, int numberOfTickets, string category, float price, int eventOrganizerId)

        {
            Name = name;
            Address = address;
            City = city;
            Date = date;
            NumberOfTickets = numberOfTickets;
            Tickets = new List<Ticket>();
            Category = category;
            Price = price;
            EventOrganizerId = eventOrganizerId;

            for (int i = 1; i <= numberOfTickets; i++)
            {
               Tickets.Add(new Ticket (price, this.Id, TicketState.Available, null));
            }
        }

        public Event(int id, string? name, string? address, string? city, DateTime date, string? category, float price)
        {
            Id = id;
            Name = name;
            Address = address;
            City = city;
            Date = date;
            Category = category;
            Price = price;
        }

        public Event() // constructor vacio que necesita ef
        {
            Tickets = new List<Ticket>();
        }

    }
}
