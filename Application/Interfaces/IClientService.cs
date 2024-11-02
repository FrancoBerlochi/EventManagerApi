using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using Domain.Entities;
using Application.Models.DTO;

namespace Application.Interfaces
{
    public interface IClientService
    {
        ClientDto CreateClient(ClientCreateRequest clientRequest);
        List<Ticket> GetAllMyTickets(int clientId);
        bool BuyTicket(int clientId, int eventId);
        void Update(int id,  ClientUpdateRequest clientUpdateRequest);
        void Delete(int id);
        List<Event> GetAll();
        Client GetClientById(int clientId);

    }
}
