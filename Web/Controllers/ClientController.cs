using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Repositories;
using Infrastructure;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Application.Models.Request;
using System.Security.Claims;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IClientService _clientService;

        public ClientController(ApplicationContext context  , IClientService clientService)
        {
            _context = context ;
            _clientService = clientService;
        }


        [HttpPost()]
        public IActionResult Create(ClientCreateRequest clientCreateRequest)
        {
            var client = _clientService.CreateClient(clientCreateRequest);
            if(client == null)
            {
                return BadRequest("Client cannot be not null");
            }
            return CreatedAtAction(nameof(GetClientById), new {id = client.Id}, client);
        }

        [HttpGet("/events/event/ticket/available")]
        public IActionResult GetAllAvailableTickets() 
        {
            var result = _clientService.GetAllAvailableTickets();
            return Ok(result);

        }

        [Authorize(Policy = "Client")]
        [HttpPost("/events/event/{eventId}/buy-ticket")]
        public IActionResult BuyTicket(int eventId)
        {
           var clientId = _clientService.GetUserInfo(User);
           var result = _clientService.BuyTicket(clientId, eventId);

            if (result) 
            {
                return Ok(new { success = true, message = "Ticket purchased successfully" });
            }
            else
            {
                return StatusCode(403, new { success = false, message = "Event does not exist or you are not a client" });
            }
        }

        [Authorize(Policy = "Client")]
        [HttpGet("client/get-tickets")]
        public IActionResult GetMyTickets()
        {
            var clientId = _clientService.GetUserInfo(User);
            var tickets = _clientService.GetAllMyTickets(clientId);

            if (tickets == null)
            {
                return NotFound("No tickets found for the client");
            }

            return Ok(tickets);
        }

        [Authorize(Policy = "Client")]
        [HttpGet("client/get-client")]
        public IActionResult GetClientById() 
        {
            var clientId = _clientService.GetUserInfo(User);
            var client = _clientService.GetClientById(clientId);
            if (client == null)
            {
                return NotFound("Client not found");
            }
            return Ok(client);
        }

        [Authorize(Policy = "SuperAdmin")]
        [HttpGet("client/get-all-clients")]

        public IActionResult GetAllClients() 
        {
            var clients = _clientService.GetAllClients();
            if (clients == null) return NotFound("No clients");
            return Ok(clients);
        }

        [HttpGet("get-all-events")]
        public IActionResult GetAllEvents()
        {
            return Ok(_clientService.GetAll());
        }


        [Authorize(Policy = "Client")]
        [HttpPut("client/update")]
        public IActionResult Update(ClientUpdateRequest clientUpdateRequest)
        {
            var clientId = _clientService.GetUserInfo(User);
            
            _clientService.Update(clientId, clientUpdateRequest);
            return NoContent();
            

        }

        [Authorize(Policy = "Client")]
        [HttpDelete("client/delete")]
        public IActionResult Delete()
        {
            var clientId = _clientService.GetUserInfo(User);
            var client = _clientService.GetClientById(clientId);
            if(client != null)
            {
                _clientService.Delete(clientId);
                return NoContent();
            }
            return NotFound("Not found");
        }

        

    }
}
