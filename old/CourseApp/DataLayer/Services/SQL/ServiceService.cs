using CourseApp.Classes;
using CourseApp.DataLayer.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Services
{
    public class ServiceService : Service
    {
        public ServiceService(IAdapter adapter)
            : base(adapter) { }

        public List<Classes.Service> GetServices()
        {
            return adapter.GetAll<Classes.Service>()
                .OrderBy(x => x.ServiceId).ToList();        
        }
        public List<string> GetTitles()
        {
            return adapter.GetAll<Classes.Service>().Select(x => x.Title).ToList();
        }
        public IEnumerable<Classes.Service> GetServicesByAgentId(int agentId)
        {
            var agents = adapter.GetAll<Agent>();
            var workers = adapter.GetAll<Worker>();
            var departaments = adapter.GetAll<Departament>();
            var dTypes = adapter.GetAll<DType>();
            var services = adapter.GetAll<Classes.Service>();

            var result =
                from agent in agents
                from worker in workers
                from service in services
                from departament in departaments
                from dType in dTypes

                where agent.AgentId == agentId
                where agent.WorkerId == worker.WorkerId
                where worker.DepartamentId == departament.DepartamentId
                where dType.DTypeId == departament.DTypeID
                where dType.DTypeId == service.DTypeId

                select service;

            return result;
        }
        public void Update(Classes.Service service) => adapter.Update(service);
        public IEnumerable<string> GetNamesByDtypeId(int dTypeId)
        {
            return adapter.GetAll<Classes.Service>()
                .Where(x => x.DTypeId == dTypeId)
                .Select(x => x.Title);
        }
        public Classes.Service GetServiceByTitleAndDTypeId(string title, int dTypeId)
        {
            return adapter.GetAll<Classes.Service>()
                .First(x => x.Title == title && x.DTypeId == dTypeId);
        }
    }
}
