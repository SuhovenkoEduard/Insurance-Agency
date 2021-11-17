using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer
{
    public class MongoDB
    {
        public IMongoDatabase Database { get; }
        protected string connectionString;
        public MongoDB(string connectionString, string dbName = "InsuranceAgency")
        {
            this.connectionString = connectionString;
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionString);
            MongoClient mongoClient = new MongoClient(settings);
            this.Database = mongoClient.GetDatabase(dbName);
        }
    }
}
