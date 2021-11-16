using CourseApp.Classes;
using CourseApp.DataLayer.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Services
{
    public class DepartamentService : Service
    {
        public DepartamentService(IAdapter adapter)
            : base(adapter) { }

        public IEnumerable<string> GetDepartamentTitlesByFilial(Filial filial)
        {
            var dTypes = adapter.GetAll<DType>();
            var departaments = adapter.GetAll<Departament>();

            var result =
                from dType in dTypes
                from departament in departaments
                where dType.DTypeId == departament.DTypeID
                where departament.FilialId == filial.FilialId
                select dType.Title;

            return result;
        }

        public Departament GetDepartamentByTitleAndFilialId(string title, int filialId)
        {
            var dTypes = adapter.GetAll<DType>();
            var departaments = adapter.GetAll<Departament>();

            var result =
                from dType in dTypes
                from departament in departaments
                where dType.DTypeId == departament.DTypeID
                where dType.Title == title
                where departament.FilialId == filialId
                select departament;

            return result.First();
        }
        public Departament GetDepartamentById(int departamentId)
        {
            var departaments = adapter.GetAll<Departament>();

            var result =
                from departament in departaments
                where departament.DepartamentId == departamentId
                select departament;

            return result.First();
        }
    }
}
