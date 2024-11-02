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
        public IActionResult Create([FromQuery] ClientCreateRequest clientCreateRequest)
        {
            var client = _clientService.CreateClient(clientCreateRequest);
            if(client != null)
            {
                return CreatedAtAction(nameof(GetClientById), new {id = client.Id}, client); 
            }
            return BadRequest();
        }


        [Authorize(Policy = "Client")]
        [HttpPost("{clientId}/events/{eventId}/buy-ticket")]
        public IActionResult BuyTicket(int clientId, int eventId)
        {
           var result = _clientService.BuyTicket(clientId, eventId);

            if (result) //arreglo temporal
            {
                return Ok("Ticket purchased succesfully");
            }
            else
            {
                return BadRequest("Failed to purchase ticket");
            }
        }

        [Authorize(Policy = "Client")]
        [HttpGet("{clientId}/get-tickets")]
        public IActionResult GetMyTickets(int clientId)
        {
            var tickets = _clientService.GetAllMyTickets(clientId);

            if (tickets == null)
            {
                return NotFound("No tickets found for the client");
            }

            return Ok(tickets);
        }

        [Authorize(Policy = "EventOrganizer")]
        [HttpGet("{Id}")]
        public IActionResult GetClientById(int Id) 
        {
            var client = _clientService.GetClientById(Id);
            if (client == null)
            {

                return NotFound("Client not found");
            }
            return Ok(client);
        }

        [HttpGet("get-all-events")]
        public IActionResult GetAllEvents()
        {
            return Ok(_clientService.GetAll());
        }


        [Authorize(Policy = "SuperAdmin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var client = _clientService.GetClientById(id);
            if(client != null)
            {
                _clientService.Delete(id);
                return NoContent();
            }
            return BadRequest("Not found");
        }

        [Authorize(Policy = "SuperAdmin")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromQuery] ClientUpdateRequest clientUpdateRequest)
        {
            try
            {
                _clientService.Update(id, clientUpdateRequest);
                return NoContent();
            } catch
            {
                return BadRequest();
            }

        }

    }
}
