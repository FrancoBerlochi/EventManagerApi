using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Infrastructure.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationContext _context;

        public ClientRepository(ApplicationContext context)
        {
            _context = context; 
        }

        public Client CreateClient(Client client)
        {
            if(client != null)
            {
                _context.Users.Add(client);
                _context.SaveChanges();
                return client;
            }
            return null;
        }
       
        public bool BuyTicket(int eventId, int clientId)
        {
            var client = _context.Users.OfType<Client>().FirstOrDefault(c => c.Id == clientId);
            var ticket = _context.Tickets.Where(t => t.EventId == eventId).FirstOrDefault(t => t.State == TicketState.Available);

            if (client != null && ticket != null)
            {
                ticket.ClientId = clientId;
                ticket.State = TicketState.Sold; 
                _context.Tickets.Update(ticket);
                client.MyTickets.Add(ticket);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Ticket> GetAllMyTickets(int clientId)
        {
            return _context.Tickets
            .Where(ticket => ticket.ClientId == clientId)
            .ToList(); 
        }

        public Client GetClientById(int id)
        {
            return _context.Users.OfType<Client>().FirstOrDefault(c => c.Id == id);
        }

        public void UpdateClient(int id, Client client)
        {
            var existingClient = _context.Clients.OfType<Client>().FirstOrDefault(c => c.Id == id);
            if(existingClient != null)
            {
                existingClient.Name = client.Name;
                existingClient.Email = client.Email;
                existingClient.Password = client.Password;
                existingClient.Phone = client.Phone;
                _context.Users.Update(existingClient);
                _context.SaveChanges();
            }
        }

        public void DeleteClient(int id)
        {
           var client =  _context.Users.OfType<Client>().FirstOrDefault(c => c.Id == id);
           if(client != null)
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();
            }
        }

        public List<Event> GetAllEvents()
        {
            return _context.Events.ToList();
        }


    }
}
