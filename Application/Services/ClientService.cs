using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.DTO;
using Application.Models.Request;

namespace Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IEventRepository _eventRepository;

        public ClientService (IClientRepository clientRepository, IEventRepository eventRepository)
        {
            _clientRepository = clientRepository;
            _eventRepository = eventRepository;
        }

        public ClientDto CreateClient(ClientCreateRequest clientCreateRequest)
        {
            var newClient = new Client(clientCreateRequest.Name, clientCreateRequest.Email, clientCreateRequest.Password, clientCreateRequest.Phone);
            var createdClient = _clientRepository.CreateClient(newClient);
            return ClientDto.Create(createdClient);
        }

        public bool BuyTicket(int clientId, int eventId)
        {
            Client client = _clientRepository.GetClientById(clientId);
            if (client == null)
            {
                return false;
            }

            Event eventEntity = _eventRepository.GetById(eventId);
            if (eventEntity == null)
            {
                return false;
            }


            _clientRepository.UpdateClient(clientId, client);
            return _clientRepository.BuyTicket(eventId, clientId);
        }

        public List<Event> GetAll() 
        { 
            return _clientRepository.GetAllEvents();
        }

        public Client GetClientById(int clientId) 
        {
            return _clientRepository.GetClientById(clientId);
        }

        public List<Ticket> GetAllMyTickets(int clientId) { return _clientRepository.GetAllMyTickets(clientId); }

        public void Update(int id, ClientUpdateRequest clientUpdateRequest)
        {
            var client = new Client(clientUpdateRequest.Name, clientUpdateRequest.Email, clientUpdateRequest.Password, clientUpdateRequest.Phone);
            _clientRepository.UpdateClient(id, client);
        }

        public void Delete(int id)
        {
            _clientRepository.DeleteClient(id);
        }
    }
}
