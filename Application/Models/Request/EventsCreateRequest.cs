using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class EventsCreateRequest()
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public DateTime Date{ get; set; }
        [Required]
        public int NumberOfTickets { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int EventOrganizerId { get; set; }
    }
}
