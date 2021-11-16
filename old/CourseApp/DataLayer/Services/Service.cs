using CourseApp.DataLayer.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Services
{
    public class Service
    {
        protected IAdapter adapter;
        public Service(IAdapter adapter)
        {
            this.adapter = adapter;
        }
    }
}
