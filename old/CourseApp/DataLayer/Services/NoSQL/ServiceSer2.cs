using CourseApp.Classes;
using CourseApp.DataLayer.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Services.NoSQL
{
    public class ServiceSer2 : ServiceService
    {
        private UserSer2 users;
        private FilialSer2 _filials;
        public ServiceSer2(IAdapter adapter, UserSer2 users, FilialSer2 filials)
            : base(adapter) 
        {
            this.users = users;
            this._filials = filials;
        }

        public override List<Classes.Service> GetServices()
        {
            return adapter.GetAll<Classes.Service>()
                .OrderBy(x => x.ServiceId).ToList();
        }
        public override List<string> GetTitles()
        {
            return adapter.GetAll<Classes.Service>().Select(x => x.Title).ToList();
        }
        public override IEnumerable<Classes.Service> GetServicesByAgentId(int agentId)
        {
            var agents = users.GetAgents();
            var workers = users.GetWorkers();
            var departaments = _filials.GetDepartaments();
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
        public override void Update(Classes.Service service) => adapter.Update(service);
        public override IEnumerable<string> GetNamesByDtypeId(int dTypeId)
        {
            return adapter.GetAll<Classes.Service>()
                .Where(x => x.DTypeId == dTypeId)
                .Select(x => x.Title);
        }
        public override Classes.Service GetServiceByTitleAndDTypeId(string title, int dTypeId)
        {
            return adapter.GetAll<Classes.Service>()
                .First(x => x.Title == title && x.DTypeId == dTypeId);
        }
    }
}
