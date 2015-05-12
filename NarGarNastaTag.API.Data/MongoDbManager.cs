using System.Linq;
using MongoDB.Driver;

namespace NarGarNastaTag.API.Data
{
    public class MongoDbManager<T>
    {
        private readonly IMongoCollection<T> _collection;
 
        public static MongoDbManager<T> MongoDb(string collectionName)
        {
            try
            {
                return new MongoDbManager<T>(collectionName, new ConnectionStringSettings());
            }
            catch
            {
                return null;
            }
        }

        private MongoDbManager(string collectionName, IConnectionStringSettings settings)
        {
            var url = new MongoUrl(settings.MongoDb);
            var client = new MongoClient(url);
            var database = client.GetDatabase(url.DatabaseName);
            _collection = database.GetCollection<T>(collectionName);
        }

        public void Save(T document)
        {
            _collection.InsertOneAsync(document);
        }

        public T Find(string id)
        {
            var filter = Builders<T>.Filter.Eq("_Id", id);
            return _collection.Find(filter).ToListAsync().Result.FirstOrDefault();
        }
    }
}