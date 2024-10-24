﻿using Application.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public void CreateEvent(string name, string address, string city, DateTime date, int numberOfTickets ,string category, float price, int eventOrganizerId)
        {
            var newEvent = new Event(name, address, city, date, numberOfTickets,category, price, eventOrganizerId);
            _eventRepository.Add(newEvent);
        }

        public Event GetEventById(int eventId)
        {
            return _eventRepository.GetById(eventId);
        }

        public List<Event> GetAllEvents() 
        { 
            return _eventRepository.GetAll().ToList();
        }

        public List<Event> GetEventsByOrganizerId(int organizerId)
        {
            return _eventRepository.GetEventsByOrganizerId(organizerId).ToList();
        }

        public void UpdateEvent(Event eventToUpdate)
        {
            _eventRepository.Update(eventToUpdate);
        }

        public void DeleteEvent(int eventId)
        {
            _eventRepository.Delete(eventId);
        }
    }
}
