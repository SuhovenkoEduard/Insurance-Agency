using CourseApp.Classes;
using CourseApp.DataLayer.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Services
{
    public class DTypeService : Service
    {
        public DTypeService(IAdapter adapter)
            : base(adapter) { }

        public DType GetDtypeById(int dTypeId)
        {
            return adapter.GetAll<DType>()
                .Find(x => x.DTypeId == dTypeId);
        }
    }
}
