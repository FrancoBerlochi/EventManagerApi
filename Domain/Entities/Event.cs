﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Event
    {
        public string Name {  get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public float Price { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<Ticket> Tickets { get; set; }
        public Event(string name, string address, string city, DateTime date, List<Ticket> tickets, string category, float price)
        {
            Name = name;
            Address = address;
            City = city;
            Date = date;
            Tickets = tickets;
            Category = category;
            Price = price;
        }

        //public Event() // constructor vacio que necesita ef
        //{
        //    Tickets = new List<Ticket>();
        //}
    }
}
