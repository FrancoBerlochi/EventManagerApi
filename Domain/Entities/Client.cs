using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client : User 
    {
        public ICollection<Ticket> MyTickets { get; set; } = new List<Ticket>();

        public Client(string name, string email, string password, string phone, string role = "Client") : base(name, email, password, phone, role)
        {
            
        }

        public Client() { }
    }
}

