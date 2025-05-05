using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class SuperAdminRepository 
    {
        private readonly ApplicationContext _context;

        public SuperAdminRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(Event eventToAdd)
        {
            _context.Events.Add(eventToAdd);
            _context.SaveChanges();
        }

        public Event GetById(int eventId)
        {
            return _context.Events.Include(e => e.EventOrganizer).FirstOrDefault(e => e.Id == eventId);
        }

        public IEnumerable<Event> GetAll()
        {
            return _context.Events.Include(e => e.EventOrganizer).ToList();
        }

        public IEnumerable<Event> GetEventsByOrganizerId(int organizerId)
        {
            return _context.Events
                .Include(e => e.EventOrganizer)
                .Where(e => e.EventOrganizerId == organizerId)
                .ToList();
        }

        public void UpdateEvent(Event eventToUpdate)
        {
            var existingEvent = _context.Events.Find(eventToUpdate.Id);
            if (existingEvent != null)
            {
                existingEvent.Name = eventToUpdate.Name;
                existingEvent.Address = eventToUpdate.Address;
                existingEvent.City = eventToUpdate.City;
                existingEvent.Date = eventToUpdate.Date;
                existingEvent.NumberOfTickets = eventToUpdate.NumberOfTickets;
                existingEvent.Category = eventToUpdate.Category;
                existingEvent.Price = eventToUpdate.Price;

                _context.Update(existingEvent);
                _context.SaveChanges();
            }
        }

        public bool Update(int id, SuperAdmin superAdmin) 
        { 
            var contextSuperAdmin = _context.Users.OfType<SuperAdmin>().FirstOrDefault(a => a.Id == id);
            contextSuperAdmin.Name = superAdmin.Name;
            contextSuperAdmin.Email = superAdmin.Email;
            contextSuperAdmin.Password = superAdmin.Password;
            contextSuperAdmin.Phone = superAdmin.Phone;
            _context.Users.Update(contextSuperAdmin);
            _context.SaveChanges();
            return true;
        }


        public void Delete(int eventId)
        {
            var eventToDelete = _context.Events.Find(eventId);
            if (eventToDelete != null)
            {
                _context.Events.Remove(eventToDelete);
                _context.SaveChanges();
            }
        }

        public void CreateEventOrganizer(EventOrganizer eventOrganizer)
        {
            _context.EventsOrganizers.Add(eventOrganizer);
            _context.SaveChanges();
        }
    }
}
