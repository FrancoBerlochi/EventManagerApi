﻿using Application.Interfaces;
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
using System.Security.Claims;

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

        public int GetUserInfo(ClaimsPrincipal User) 
        {
            var claimId = int.Parse(User.FindFirst("NameIdentifier")?.Value);
            return claimId;
        }

        public ClientDto CreateClient(ClientCreateRequest clientCreateRequest)
        {
            var newClient = new Client(clientCreateRequest.Name, clientCreateRequest.Email, clientCreateRequest.Password, clientCreateRequest.Phone);
            var createdClient = _clientRepository.CreateClient(newClient);
            return ClientDto.Create(createdClient);
        }

        public List<Object> GetAllAvailableTickets() 
        { 
            var events =  _clientRepository.GetAllAvailableTickets();
            var allAvailableEvents = new List<Object>();
            for (int i = 0; i < events.Count; i++)
            {
                var countTickets = events[i].Tickets.Count(t => t.State == TicketState.Available);
               
                 allAvailableEvents.Add(new { events[i].Id, availableTickets = countTickets });
                
            }
            return allAvailableEvents;
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

        public List<ClientDto> GetAllClients() 
        { 
            var clients = _clientRepository.GetAllClients();
            if (clients == null) 
            {
                return null;
            }
            var clientsDto = new List<ClientDto>();
            foreach (var client in clients) 
            {
                clientsDto.Add(ClientDto.Create(client));
            }
            return clientsDto;
        }
        public List<TicketDto> GetAllMyTickets(int clientId) 
        { 
            var tickets = _clientRepository.GetAllMyTickets(clientId);
            if (tickets == null)
            {
                return null;
            }
            var ticketsDto = new List<TicketDto>();
            foreach (var ticket in tickets) 
            { 
                ticketsDto.Add(TicketDto.create(ticket));
            }

            return ticketsDto;
        }

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
