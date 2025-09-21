using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace perla_metro_tickets_service.src.DTOs
{
    public class TicketDto
    {
        public Guid Id { get; set; }

        public Guid PassagerId { get; set; }

        //Fecha/hora exacta de emisión
        public DateTime IssuedAt { get; set; }

        //Fecha normalizada 
        public DateTime IssuedDate { get; set; }

        //Tipo de ticket
        public string Type { get; set; } = string.Empty;

        //Estado del ticket
        public string State { get; set; } = string.Empty;

        //Monto pagado
        public decimal Amount { get; set; }

        //Fecha creada
        public DateTime CreatedAt { get; set; }
    }
}