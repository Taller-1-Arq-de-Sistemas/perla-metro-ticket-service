using System.ComponentModel.DataAnnotations;
using perla_metro_tickets_service.src.models;

namespace perla_metro_tickets_service.src.DTOs
{
    public class CreateTicketDto
    {
        [Required(ErrorMessage = "El ID del pasajero es obligatorio.")]
        public required Guid PassengerId { get; set; }

        public required DateTime IssuedAt { get; set; }

        [Required(ErrorMessage = "El tipo de ticket es obligatorio.")]
        [EnumDataType(typeof(TicketType), ErrorMessage = "El tipo de ticket no es válido.")]
        public required string Type { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [EnumDataType(typeof(TicketState), ErrorMessage = "El estado del ticket no es válido.")]
        public required string Status { get; set; } = "Activo";

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que 0.")]
        public required decimal Amount { get; set; }
    }
}