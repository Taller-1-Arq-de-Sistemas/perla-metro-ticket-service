using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace perla_metro_tickets_service.src.DTOs
{
    public class ViewTicketDto
    {
        public Guid Id { get; set; }

        public string PassengerName { get; set; } = string.Empty;

        public DateTime IssuedAt { get; set; }

        public string Type { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public decimal Amount { get; set; }
    }
}