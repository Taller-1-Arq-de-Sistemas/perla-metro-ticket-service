using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using perla_metro_tickets_service.src.Data;
using perla_metro_tickets_service.src.models;

namespace perla_metro_tickets_service.src.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IMongoCollection<Ticket> _collection;

        public TicketRepository(MongoContext context)
        {
            _collection = context.Tickets;
        }

        public async Task<bool> CreatedAsync(Ticket ticket)
        {
            await _collection.InsertOneAsync(ticket);
            return true;
        }

        public async Task<bool> ExistisDuplicateAsync(Guid passagerId, DateTime issuedDate)
        {
            var filter = Builders<Ticket>.Filter.And(
                Builders<Ticket>.Filter.Eq(t => t.PassagerId, passagerId),
                Builders<Ticket>.Filter.Eq(t => t.IssuedDate, issuedDate),
                Builders<Ticket>.Filter.Eq(t => t.IsDeleted, false)
            );

            var count = await _collection.CountDocumentsAsync(filter);

            return count > 0;
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _collection.Find(t => !t.IsDeleted).ToListAsync();
        }

        public async Task<Ticket?> GetByIdAsync(Guid id)
        {
            return await _collection.Find(t => t.Id == id && !t.IsDeleted).FirstOrDefaultAsync();
        }

        public async Task<bool> SoftDeleteAsync(Guid id, string deletedBy)
        {
            var update = Builders<Ticket>.Update
                            .Set(t => t.IsDeleted, true)
                            .Set(t => t.DeletedAt, DateTime.UtcNow)
                            .Set(t => t.DeletedBy, deletedBy);

            await _collection.UpdateOneAsync(t => t.Id == id, update);

            return true;
        }

        public async Task<bool> UpdateAsync(Ticket ticket)
        {
            await _collection.ReplaceOneAsync(t => t.Id == ticket.Id, ticket);
            return true;
        }
    }
}