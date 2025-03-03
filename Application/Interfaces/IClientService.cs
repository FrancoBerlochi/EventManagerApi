using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using Domain.Entities;
using Application.Models.DTO;
using System.Security.Claims;

namespace Application.Interfaces
{
    public interface IClientService
    {
        int GetUserInfo(ClaimsPrincipal User);
        ClientDto CreateClient(ClientCreateRequest clientRequest);
        List<TicketDto> GetAllMyTickets(int clientId);
        bool BuyTicket(int clientId, int eventId);
        void Update(int id,  ClientUpdateRequest clientUpdateRequest);
        void Delete(int id);
        List<Event> GetAll();
        Client GetClientById(int clientId);
        List<ClientDto> GetAllClients();

    }
}
