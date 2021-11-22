using CourseApp.Classes;
using CourseApp.DataLayer.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Services.NoSQL
{
    public class AgentSer2 : AgentService
    {
        protected UserSer2 users;
        protected FilialSer2 _filials;
        public AgentSer2(IAdapter adapter, UserSer2 users, FilialSer2 filials)
            : base(adapter)
        {
            this.users = users;
            this._filials = filials;
        }

        public override int GetDepartamentIdByAgentId(int agentId)
        {
            var workers = users.GetWorkers();
            var agents = users.GetAgents();

            var result =
                from worker in workers
                from agent in agents

                where agent.AgentId == agentId
                where agent.WorkerId == worker.WorkerId
                select worker.DepartamentId;

            return result.First();
        }
        public override Agent GetAgentByUserId(int userId) => users.GetUserById(userId).Worker.Agent;
        public override string GetFullNameById(int agentId)
        {
            var agents = users.GetAgents();
            var workers = users.GetWorkers();

            var result =
                from agent in agents
                from worker in workers
                where agent.WorkerId == worker.WorkerId
                where agent.AgentId == agentId
                select worker.FullName;

            return result.First();
        }
        public override string GetFullInfoById(int agentId)
        {
            var agents = users.GetAgents();
            var workers = users.GetWorkers();
            var departaments = _filials.GetDepartaments();
            var dTypes = adapter.GetAll<DType>();
            var filials = adapter.GetAll<Classes.NoSQL.Filial>();

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
        public override bool ContainsWorkerId(int workerId) => users.GetAgents().Any(x => x.WorkerId == workerId);
        public override int GetSalary(int agentId)
        {
            var agent = users.GetAgents().First(x => x.AgentId == agentId);
            var worker = users.GetWorkers().First(x => x.WorkerId == agent.WorkerId);

            var contracts = adapter.GetAll<Classes.NoSQL.Contract>();
            var result =
                from contract in contracts
                where contract.Worker.Agent.AgentId == agentId
                select contract.Cost;

            return result.Sum() + worker.MinSalary;
        }
        public override IEnumerable<string> GetFullNamesByDepartamentId(int departamentId)
        {
            var agents = users.GetAgents();
            var workers = users.GetWorkers();

            var result =
                from agent in agents
                from worker in workers
                where agent.WorkerId == worker.WorkerId
                where worker.DepartamentId == departamentId
                select worker.FullName;

            return result;
        }
        public override Agent GetAgentByFullnameAndDepartamentId(string fullName, int departamentId)
        {
            var agents = users.GetAgents();
            var workers = users.GetWorkers();

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
