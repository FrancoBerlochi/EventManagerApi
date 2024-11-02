using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IClientRepository
    {
        Client CreateClient(Client client);
        bool BuyTicket(int eventId, int clientId);
        List<Ticket> GetAllMyTickets(int clientId);
        Client GetClientById(int id);
        void UpdateClient(int id, Client client);
        void DeleteClient(int id);
        List<Event> GetAllEvents();
    }
}
