using CourseApp.Classes;
using CourseApp.DataLayer.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Services
{
    public class FilialService : Service
    {
        public FilialService(IAdapter adapter)
            : base(adapter) { }

        public virtual List<string> GetCities()
        {
            return adapter.GetAll<Filial>().Select(x => x.City).Distinct().ToList();
        }
        public virtual Filial GetFilialByCity(string city)
        {
            return adapter.GetAll<Filial>().First(x => x.City == city);
        }
    }
}
