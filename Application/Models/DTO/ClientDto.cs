using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Models.DTO
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;


        public static ClientDto Create(Client clientToCreate)
        {
            ClientDto dto = new ClientDto();
            dto.Id = clientToCreate.Id;
            dto.Name = clientToCreate.Name;
            dto.Email = clientToCreate.Email;
            dto.Password = clientToCreate.Password;
            dto.Phone = clientToCreate.Phone;

            return dto;
        }
    }
}
