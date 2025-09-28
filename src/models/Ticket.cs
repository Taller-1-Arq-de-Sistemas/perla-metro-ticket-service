using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace perla_metro_tickets_service.src.models
{
    public enum TicketType {one_way, round_trip}
    public enum TicketState { Active, Used, Expired}
    public class Ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid PassagerId { get; set; }

        //Fecha y hora de emición del ticket
        public DateTime IssuedAt { get; set; }
        public DateTime IssuedDate { get; set; } //Fecha y hora de emisión normalizada

        //Tipo de ticket
        public TicketType Type { get; set; }
        //Estado del ticket
        public TicketState State { get; set; }
        //Monto pagado por el ticket
        public decimal Amount { get; set; }
        //eliminación (soft Delete) del ticket
        public bool IsDeleted { get; set; } = false; //False = Activo, True = Borrado

        //ATRIBUTOS DE AUDITORIA
        //Fecha de eliminiación del ticket
        public DateTime? DeletedAt { get; set; }
        //Responsable de la eliminación del ticket
        public string? DeletedBy { get; set; }
        //Fecha de creación del ticket
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //Responsable de la craeción del ticket
        public string? CreatedBy { get; set; }
        //Fecha de edición del ticket
        public DateTime? UpdateAt { get; set; }
        //Responsable de la edición del ticket
        public string? UpdateBy { get; set; }
    }
}