using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Models.DTO;

namespace Infrastructure.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationContext _context;

        public EventRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Event Add(Event eventToAdd, int eventOrganizerId)
        {
            var eventOrganizer = _context.EventsOrganizers.Find(eventOrganizerId);
            var listEvent = _context.Events.ToList();
            var existingEvent = true;
            foreach (var e in listEvent)
            {
                if (e.Name == eventToAdd.Name && e.City == eventToAdd.City && e.Address == eventToAdd.Address && e.Date == eventToAdd.Date)
                {
                    existingEvent = false;
                }
            }

            if(eventToAdd != null && eventOrganizer != null && existingEvent)
            {
                _context.Events.Add(eventToAdd);
                eventOrganizer.MyEvents.Add(eventToAdd);
                _context.SaveChanges();
                return eventToAdd;
            }

            return null;
        }
        
        public Event GetById(int eventId)
        {
            return _context.Events.FirstOrDefault(e => e.Id == eventId);
        }

        public IEnumerable<Event> GetAll() 
        {
            return _context.Events.ToList();
        }

        public IEnumerable<Event> GetEventsByOrganizerId(int organizerId)
        {
            return _context.Events
                .Where(e => e.EventOrganizerId == organizerId)
                .ToList();
        }

        public Event Update(Event eventToUpdate, int organizerId)
        {
            var existingEvent = _context.Events.Find(eventToUpdate.Id);
            if(existingEvent != null && organizerId == existingEvent.EventOrganizerId)
            {
                existingEvent.Name = eventToUpdate.Name;
                existingEvent.Address = eventToUpdate.Address;
                existingEvent.City = eventToUpdate.City;
                existingEvent.Date = eventToUpdate.Date;
                existingEvent.Category = eventToUpdate.Category;
                existingEvent.Price = eventToUpdate.Price;
                
                _context.Update(existingEvent);
                _context.SaveChanges();
            }
            return existingEvent;
        }

        public int Delete(int eventId, int organizerId)
        {
            var eventToDelete = _context.Events.Find(eventId);

            if (eventToDelete != null && eventToDelete.EventOrganizerId == organizerId)
            {
                _context.Events.Remove(eventToDelete);
                _context.SaveChanges();
                return 1;
            }
            else if (eventToDelete == null) 
            {
                return 0;
            }  
            return -1;
        }
    }   
}
