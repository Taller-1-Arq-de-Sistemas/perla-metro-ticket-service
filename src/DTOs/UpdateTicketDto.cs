using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using perla_metro_tickets_service.src.models;

namespace perla_metro_tickets_service.src.DTOs
{
    public class UpdateTicketDto
    {
        [Required(ErrorMessage = "El estado del ticket es obligatorio.")]
        [EnumDataType(typeof(TicketState), ErrorMessage = "El estado del ticket no es válido.")]
        public TicketState State { get; set; }

        [Required(ErrorMessage = "El tipo de ticket es obligatorio.")]
        [EnumDataType(typeof(TicketType), ErrorMessage = "El tipo de ticket no es válido.")]
        public TicketType Type { get; set; }

        [Required(ErrorMessage = "La fecha y hora de emisión es obligatoria.")]
        public DateTime IssuedAt { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que 0.")]
        public decimal Amount { get; set; }
    }
}