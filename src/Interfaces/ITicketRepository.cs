using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using perla_metro_tickets_service.src.models;

namespace perla_metro_tickets_service.src.Repositories
{
    public interface ITicketRepository
    {
        Task<bool> CreatedAsync(Ticket ticket);
        Task<Ticket?> GetByIdAsync(Guid id);
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task<bool> UpdateAsync(Ticket ticket);
        Task<bool> SoftDeleteAsync(Guid id, string deletedBy);
        Task<bool> ExistisDuplicateAsync(Guid passagerId, DateTime issuedDate);

    }
}