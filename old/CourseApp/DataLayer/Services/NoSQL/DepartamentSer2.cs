using CourseApp.Classes;
using CourseApp.DataLayer.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Services.NoSQL
{
    public class DepartamentSer2 : DepartamentService
    {
        private FilialSer2 filials;
        public DepartamentSer2(IAdapter adapter, FilialSer2 filials)
            : base(adapter) 
        {
            this.filials = filials;
        }

        public override IEnumerable<string> GetDepartamentTitlesByFilial(Filial filial)
        {
            var dTypes = adapter.GetAll<DType>();
            var departaments = filials.GetDepartaments();

            var result =
                from dType in dTypes
                from departament in departaments
                where dType.DTypeId == departament.DTypeID
                where departament.FilialId == filial.FilialId
                select dType.Title;

            return result;
        }
        public override Departament GetDepartamentByTitleAndFilialId(string title, int filialId)
        {
            var dTypes = adapter.GetAll<DType>();
            var departaments = filials.GetDepartaments();

            var result =
                from dType in dTypes
                from departament in departaments
                where dType.DTypeId == departament.DTypeID
                where dType.Title == title
                where departament.FilialId == filialId
                select departament;

            return result.First();
        }
        public override Departament GetDepartamentById(int departamentId)
        {
            var departaments = filials.GetDepartaments();

            var result =
                from departament in departaments
                where departament.DepartamentId == departamentId
                select departament;

            return result.First();
        }
    }
}
