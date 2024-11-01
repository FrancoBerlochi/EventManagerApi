using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{


    public class EventOrganizer : User
    {
        public List<Event> MyEvents { get; set; } = new List<Event>();
   
        public EventOrganizer() { }
        public EventOrganizer(string name, string email, string password, string phone, string role = "EventOrganizer") : base(name, email, password, phone, role)
        {

        }
        

    }
}
