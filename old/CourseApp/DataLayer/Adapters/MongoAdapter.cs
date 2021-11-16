using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Adapters
{
    public class MongoAdapter : IAdapter
    {
        public void Add<T>(T item) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll<T>() where T : class, new()
        {
            throw new NotImplementedException();
        }

        public void Remove<T>(T item) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T item) where T : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
