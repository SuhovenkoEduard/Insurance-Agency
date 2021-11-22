using CourseApp.Classes;
using CourseApp.DataLayer.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Services
{
    public class ManagerService : Service
    {
        private AgentService agentService;
        public ManagerService(IAdapter adapter, AgentService agentService)
            : base(adapter) 
        {
            this.agentService = agentService;
        }
        public ManagerService(IAdapter adapter)
            : base(adapter) { }

        public virtual int GetDepartamentIdByManagerId(int managerId)
        {
            var workers = adapter.GetAll<Worker>();
            var managers = adapter.GetAll<Manager>();

            var result =
                from worker in workers
                from manager in managers

                where manager.ManagerId == managerId
                where manager.WorkerId == worker.WorkerId
                select worker.DepartamentId;

            return result.First();
        }
        public virtual Manager GetManagerByUserId(int userId)
        {
            var managers = adapter.GetAll<Manager>();
            var workers = adapter.GetAll<Worker>();

            var result =
                from manager in managers
                from worker in workers

                where manager.WorkerId == worker.WorkerId
                where worker.UserId == userId

                select manager;

            return result.First();
        }
        public virtual int GetSalary(int managerId)
        {
            var manager = adapter.GetAll<Manager>().First(x => x.ManagerId == managerId);
            var managerWorker = adapter.GetAll<Worker>().First(x => x.WorkerId == manager.WorkerId);
            int departamentId = GetDepartamentIdByManagerId(managerId);
            
            var contracts = adapter.GetAll<Contract>();
            var workers = adapter.GetAll<Worker>();
            var agents = adapter.GetAll<Agent>();

            var result =
               (from contract in contracts
                from agent in agents
                from worker in workers
                
                where worker.WorkerId == agent.WorkerId
                where contract.AgentId == agent.AgentId
                where worker.DepartamentId == departamentId
                
                select agent.AgentId).Distinct();

            return (int)(result.Select(x => agentService.GetSalary(x)).Sum() * 0.3 + managerWorker.MinSalary);
        }
        public virtual string GetFullInfoById(int managerId)
        {
            var managers = adapter.GetAll<Manager>();
            var workers = adapter.GetAll<Worker>();
            var departaments = adapter.GetAll<Departament>();
            var dTypes = adapter.GetAll<DType>();
            var filials = adapter.GetAll<Filial>();

            var result =
                from manager in managers
                from worker in workers
                from departament in departaments
                from dType in dTypes
                from filial in filials

                where manager.WorkerId == worker.WorkerId
                where departament.FilialId == filial.FilialId
                where departament.DTypeID == dType.DTypeId
                where worker.DepartamentId == departament.DepartamentId

                where manager.ManagerId == managerId

                select new
                {
                    manager.ManagerId,
                    worker.FullName,
                    worker.MinSalary,
                    Departament = dType.Title,
                    FilialCity = filial.City
                };

            return result.First().ToString();
        }
        public virtual bool ContainsWorkerId(int workerId)
        {
            return adapter.GetAll<Manager>().Any(x => x.WorkerId == workerId);
        }
    }
}
