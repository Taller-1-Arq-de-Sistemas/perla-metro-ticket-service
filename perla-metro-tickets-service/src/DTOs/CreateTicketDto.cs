using System.ComponentModel.DataAnnotations;

namespace perla_metro_tickets_service.src.DTOs
{
    public class CreateTicketDto
    {
        [Required(ErrorMessage = "El ID del pasajero es obligatorio.")]
        public required Guid PassengerId { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria.")]
        [DataType(DataType.DateTime, ErrorMessage = "La fecha debe tener un formato válido.")]
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "El tipo de ticket es obligatorio.")]
        [RegularExpression("^(one_way|round_trip)$", 
            ErrorMessage = "El tipo debe ser ida o vuelta.")]
        public required string Type { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [RegularExpression("^(Active|Used|Expired)$", 
            ErrorMessage = "El estado debe ser activo, Usado o caducado.")]
        public required string Status { get; set; } = "Activo";

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que 0.")]
        public required decimal Amount { get; set; }
    }
}