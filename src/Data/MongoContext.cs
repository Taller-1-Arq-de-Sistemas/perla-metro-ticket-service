using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using perla_metro_tickets_service.src.models;

namespace perla_metro_tickets_service.src.Data
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDb"));
            _database = client.GetDatabase(config["MongoDbSettings:TicketServiceDb"]);

            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }

        public IMongoCollection<Ticket> Tickets => _database.GetCollection<Ticket>("tickets");
    }
}