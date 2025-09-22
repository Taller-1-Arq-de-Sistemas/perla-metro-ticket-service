using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace perla_metro_tickets_service.src.DTOs
{
    public class PassagerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}