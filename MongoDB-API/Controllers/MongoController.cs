using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB_API.Models;
using Microsoft.Extensions.Configuration;

namespace MongoDB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MongoController : ControllerBase
    {
        readonly MongoClient client;

        public MongoController(IConfiguration configuration)
        {
            string connectionUri = configuration.GetConnectionString("DefaultMongo") ?? string.Empty;
            client = new MongoClient(connectionUri);
        }

        [HttpGet]
        [Route("GetCatalog")]
        public IEnumerable<Catalog> GetCatalog()
        {
            IMongoCollection<Catalog> collection = client.GetDatabase("Practice").GetCollection<Catalog>("Catalog");
            List<Catalog> listCatalog = collection.Find(Builders<Catalog>.Filter.Empty).ToList();
            return listCatalog;
        }

        [HttpPost]
        [Route("InsertCatalog")]
        public void InsertCatalog([FromBody] Catalog catalog)
        {
            IMongoCollection<Catalog> collection = client.GetDatabase("Practice").GetCollection<Catalog>("Catalog");
            collection.InsertOne(catalog);
        }

        [HttpPut]
        [Route("UpdateCatalog")]
        public Catalog UpdateCatalog([FromRoute] Guid id, [FromBody] Catalog catalog)
        {
            FilterDefinition<Catalog> findFilter = Builders<Catalog>.Filter.Eq(x => x.Id, id);
            UpdateDefinition<Catalog> updateFilter = Builders<Catalog>.Update.Set(x => x, catalog);
            FindOneAndUpdateOptions<Catalog, Catalog> options = new FindOneAndUpdateOptions<Catalog, Catalog>()
            {
                ReturnDocument = ReturnDocument.After
            };
            IMongoCollection<Catalog> collection = client.GetDatabase("Practice").GetCollection<Catalog>("Catalog");
            Catalog updatedCatalog = collection.FindOneAndUpdate(findFilter, updateFilter, options);
            return updatedCatalog;
        }

        [HttpDelete]
        [Route("DeleteCatalog")]
        public DeleteResult DeleteCatalog([FromRoute] Guid id)
        {
            IMongoCollection<Catalog> collection = client.GetDatabase("Practice").GetCollection<Catalog>("Catalog");
            DeleteResult result = collection.DeleteOne(Builders<Catalog>.Filter.Eq(x => x.Id, id));
            return result;
        }
    }
}
