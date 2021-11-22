using CourseApp.Classes;
using CourseApp.DataLayer.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Services.NoSQL
{
    public class ManagerSer2 : ManagerService
    {
        private UserSer2 users;
        private FilialSer2 _filials;
        private AgentSer2 _agents;
        public ManagerSer2(IAdapter adapter, UserSer2 users, FilialSer2 filials, AgentSer2 agents)
            : base(adapter) 
        {
            this.users = users;
            this._filials = filials;
            this._agents = agents;
        }

        public override int GetDepartamentIdByManagerId(int managerId)
        {
            var workers = users.GetWorkers();
            var managers = users.GetManagers();

            var result =
                from worker in workers
                from manager in managers

                where manager.ManagerId == managerId
                where manager.WorkerId == worker.WorkerId
                select worker.DepartamentId;

            return result.First();
        }
        public override Manager GetManagerByUserId(int userId)
        {
            var managers = users.GetManagers();
            var workers = users.GetWorkers();

            var result =
                from manager in managers
                from worker in workers

                where manager.WorkerId == worker.WorkerId
                where worker.UserId == userId

                select manager;

            return result.First();
        }
        public override int GetSalary(int managerId)
        {
            var manager = users.GetManagers().First(x => x.ManagerId == managerId);
            var managerWorker = users.GetWorkers().First(x => x.WorkerId == manager.WorkerId);
            int departamentId = GetDepartamentIdByManagerId(managerId);

            var contracts = adapter.GetAll<Classes.NoSQL.Contract>();
            var workers = users.GetWorkers();
            var agents = users.GetAgents();

            var result =
               (from contract in contracts
                from agent in agents
                from worker in workers

                where worker.WorkerId == agent.WorkerId
                where contract.Worker.Agent.AgentId == agent.AgentId
                where worker.DepartamentId == departamentId

                select agent.AgentId).Distinct();

            return (int)(result.Select(x => _agents.GetSalary(x)).Sum() * 0.3 + managerWorker.MinSalary);
        }
        public override string GetFullInfoById(int managerId)
        {
            var managers = users.GetManagers();
            var workers = users.GetWorkers();
            var departaments = _filials.GetDepartaments();
            var dTypes = adapter.GetAll<DType>();
            var filials = adapter.GetAll<Classes.NoSQL.Filial>();

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

                select
                    "Айди менеджера: " + manager.ManagerId + "\n" +
                    "ФИО:" + worker.FullName + "\n" +
                    "Зарплата: " + worker.MinSalary + "\n" +
                    "Отдел: " + dType.Title + "\n" +
                    "Город: " + filial.City;
            return result.First();
        }
        public override bool ContainsWorkerId(int workerId) => users.GetManagers().Any(x => x.WorkerId == workerId);
    }
}
