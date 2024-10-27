﻿using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class EventOrganizerRepository : IEventOrganizerRepository
    {
        private readonly ApplicationContext _context;
        public EventOrganizerRepository(ApplicationContext context) 
        {
            _context = context;
        }
        public EventOrganizer GetEventOrganizer(int eventOrganizerId) 
        {
            return _context.Users.OfType<EventOrganizer>().FirstOrDefault(e => e.Id == eventOrganizerId);
        }

        public int CheckAvailableTickets(int eventOrganizerId, int eventId)
        {
            var myEvent = _context.Events.Include(t => t.Tickets).FirstOrDefault(e => e.Id == eventId);
            var organizer = _context.Users.OfType<EventOrganizer>().FirstOrDefault(e => e.Id == eventOrganizerId);

            if (myEvent == null)
            {
                return -1;
            }
            else if (organizer == null)
            {
                return -2;
            }

            return myEvent.Tickets.Count(t => t.State == TicketState.Available);
        }

        public int CheckSoldTickets(int eventOrganizerId, int eventId)
        {
            var myEvent = _context.Events.Include(t => t.Tickets).FirstOrDefault(e => e.Id == eventId);
            var organizer = _context.Users.OfType<EventOrganizer>().FirstOrDefault(e => e.Id == eventOrganizerId);

            if (myEvent == null)
            {
                return -1;
            }
            else if (organizer == null)
            {
                return -2;
            }

            return myEvent.Tickets.Count(t => t.State == TicketState.Sold);
        }
    }
}
