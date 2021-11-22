using CourseApp.Classes;
using CourseApp.DataLayer.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Services
{
    public class AgentService : Service
    {
        public AgentService(IAdapter adapter) 
            : base(adapter) { }

        public virtual int GetDepartamentIdByAgentId(int agentId)
        {
            var workers = adapter.GetAll<Worker>();
            var agents = adapter.GetAll<Agent>();

            var result =
                from worker in workers
                from agent in agents

                where agent.AgentId == agentId
                where agent.WorkerId == worker.WorkerId
                select worker.DepartamentId;

            return result.First();
        }
        public virtual Agent GetAgentByUserId(int userId)
        {
            var agents = adapter.GetAll<Agent>();
            var workers = adapter.GetAll<Worker>();

            var result =
                from agent in agents
                from worker in workers
                where agent.WorkerId == worker.WorkerId
                where worker.UserId == userId
                select agent;

            return result.First();
        }
        public virtual string GetFullNameById(int agentId)
        {
            var agents = adapter.GetAll<Agent>();
            var workers = adapter.GetAll<Worker>();

            var result =
                from agent in agents
                from worker in workers
                where agent.WorkerId == worker.WorkerId
                where agent.AgentId == agentId
                select worker.FullName;

            return result.First();
        }
        public virtual string GetFullInfoById(int agentId)
        {
            var agents = adapter.GetAll<Agent>();
            var workers = adapter.GetAll<Worker>();
            var departaments = adapter.GetAll<Departament>();
            var dTypes = adapter.GetAll<DType>();
            var filials = adapter.GetAll<Filial>();

            var result =
                from agent in agents
                from worker in workers
                from departament in departaments
                from dType in dTypes
                from filial in filials

                where agent.WorkerId == worker.WorkerId
                where departament.FilialId == filial.FilialId
                where departament.DTypeID == dType.DTypeId
                where worker.DepartamentId == departament.DepartamentId

                where agent.AgentId == agentId

                select new
                {
                    agent.AgentId,
                    worker.FullName,
                    worker.MinSalary,
                    Departament = dType.Title,
                    FilialCity = filial.City
                };

            return result.First().ToString();
        }
        public virtual bool ContainsWorkerId(int workerId)
        {
            return adapter.GetAll<Agent>().Any(x => x.WorkerId == workerId);
        }
        public virtual int GetSalary(int agentId)
        {
            var agent = adapter.GetAll<Agent>().First(x => x.AgentId == agentId);
            var worker = adapter.GetAll<Worker>().First(x => x.WorkerId == agent.WorkerId);

            var contracts = adapter.GetAll<Contract>();
            var result =
                from contract in contracts
                where contract.AgentId == agentId
                select contract.Cost;

            return result.Sum() + worker.MinSalary;
        }
        public virtual IEnumerable<string> GetFullNamesByDepartamentId(int departamentId)
        {
            var agents = adapter.GetAll<Agent>();
            var workers = adapter.GetAll<Worker>();

            var result =
                from agent in agents
                from worker in workers
                where agent.WorkerId == worker.WorkerId
                where worker.DepartamentId == departamentId
                select worker.FullName;

            return result;
        }
        public virtual Agent GetAgentByFullnameAndDepartamentId(string fullName, int departamentId)
        {
            var agents = adapter.GetAll<Agent>();
            var workers = adapter.GetAll<Worker>();

            var result =
                from agent in agents
                from worker in workers
                where agent.WorkerId == worker.WorkerId
                where worker.FullName == fullName
                where worker.DepartamentId == departamentId
                select agent;

            return result.First();
        }
    }
}
