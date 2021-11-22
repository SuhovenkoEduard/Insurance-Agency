using CourseApp.Classes;
using CourseApp.DataLayer.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Services.NoSQL
{
    public class FilialSer2 : FilialService
    {
        public FilialSer2(IAdapter adapter)
            : base(adapter) { }

        public override List<string> GetCities() => adapter.GetAll<Classes.NoSQL.Filial>().Select(x => x.City).Distinct().ToList();
        public override Filial GetFilialByCity(string city)
        {
            return adapter.GetAll<Classes.NoSQL.Filial>()
                .Where(x => x.City == city)
                .Select(filial => new Filial(filial.NumberOfWorkers, filial.City, filial.DateOfCreation))
                .First();
        }

        // additional
        public List<Departament> GetDepartaments()
        {
            var departments = new List<Departament>();
            adapter.GetAll<Classes.NoSQL.Filial>()
                .Select(filial => filial.Departments)
                .ToList().ForEach(departmentByFilial => departments.AddRange(departmentByFilial));
            return departments;
        }
    }
}
