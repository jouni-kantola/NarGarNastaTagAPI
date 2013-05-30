using System.Configuration;

namespace NarGarNastaTag.API.Data
{
    class ConnectionStringSettings : IConnectionStringSettings
    {
        public string MongoDb
        {
            get { return ConfigurationManager.AppSettings["MONGO_DB"]; }
        }
    }
}