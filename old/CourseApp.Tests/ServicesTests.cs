using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;
using CourseApp.DataLayer.Adapters;
using CourseApp.DataLayer;
using CourseApp.Classes;

namespace CourseApp.Tests
{
    [TestClass]
    public class ServicesTests
    {
        protected IAdapter adapter;
        protected SQLLayer dataLayer;

        public void Init()
        {
            /*DataLayer.MongoDB mongo = new DataLayer.MongoDB("mongodb://localhost/");
            adapter = new MongoAdapter(mongo);
            dataLayer = new NoSQLLayer(adapter);*/
            AccessDB access = new AccessDB("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\\..\\db\\CourseDB.mdb");
            adapter = new AccessAdapter(access);
            dataLayer = new SQLLayer(adapter);
        }
        
        [TestMethod]
        public void AddNewContract_WhenTheAllItemsIsCorrect_ShouldReturnNewItemethod1()
        {
            Init();
            var contracts = adapter.GetAll<Contract>();
            //var users = adapter.GetAll<Classes.NoSQL.User>();
            //Assert.IsTrue(users.Any(user => user.UserId == 1));
        }
    }
}
