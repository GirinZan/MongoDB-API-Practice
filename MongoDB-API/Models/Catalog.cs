using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDB_API.Models
{
    public class Catalog
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Amount { get; set; } = 0;
        public int Price { get; set; } = 0;
        public List<int> PriceHistory { get; set; } = new List<int>();
    }
}
