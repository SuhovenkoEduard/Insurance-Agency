using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Adapters
{
    public interface IAdapter
    {
        void Add<T>(T item)
            where T : class, new();
        void Update<T>(T item)
            where T : class, new();
        void Remove<T>(T item)
            where T : class, new();
        List<T> GetAll<T>()
            where T : class, new();
    }
}
