using CourseApp.Classes;
using CourseApp.DataLayer.Adapters;
using System.Collections.Generic;
using System.Linq;

namespace CourseApp.DataLayer.Services.NoSQL
{
    public class ContractSer2 : ContractService
    {
        public UserSer2 users;
        public FilialSer2 filials;
        public ContractSer2(IAdapter adapter, UserSer2 users, FilialSer2 filials)
            : base(adapter) 
        {
            this.users = users;
            this.filials = filials;
        } 

        // [-]
        public override void Add(Contract contract)
        {
            adapter.Add(contract);
        }
        // [-]
        public override void Update(Contract contract) => adapter.Update(contract);
        // [-]
        public override void Remove(Contract contract) => adapter.Remove(contract);
        public override Contract GetContractById(int contractId)
        {
            return adapter.GetAll<Classes.NoSQL.Contract>()
                .Where(x => x.ContractId == contractId)
                .Select(contract => contract.ToSQLContract())
                .First();
        }


        // by client
        public override IEnumerable<object> GetFullInfoByClientId(int clientId)
        {
            var contracts = adapter.GetAll<Classes.NoSQL.Contract>();
            var agents = users.GetAgents();
            var workers = users.GetWorkers();
            var services = adapter.GetAll<Classes.Service>();
            var statuses = adapter.GetAll<Status>();

            var result =
                from contract in contracts
                from agent in agents
                from worker in workers
                from service in services
                from status in statuses

                where agent.WorkerId == worker.WorkerId

                where contract.Worker.Agent.AgentId == agent.AgentId
                where contract.ServiceId == service.ServiceId
                where contract.Status.StatusId == status.StatusId

                where contract.Client.ClientId == clientId

                select new
                {
                    contract.ContractId,
                    contract.DateOfConclusion,
                    ServiceName = service.Title,
                    AgentName = worker.FullName,
                    contract.Cost,
                    contract.InsurancePeriod,
                    contract.Comment,
                    Status = status.Title
                };

            return result.OrderBy(x => x.ContractId);
        }
        public override IEnumerable<int> GetContractIdByClientId(int clientId)
        {
            var contracts = adapter.GetAll<Classes.NoSQL.Contract>();

            var result =
                from contract in contracts
                where contract.Client.ClientId == clientId
                orderby contract.ContractId
                select contract.ContractId;

            return result;
        }


        // by agent
        public override IEnumerable<object> GetFullInfoByAgentId(int agentId)
        {
            var contracts = adapter.GetAll<Classes.NoSQL.Contract>();
            var clients = users.GetClients();
            var services = adapter.GetAll<Classes.Service>();
            var statuses = adapter.GetAll<Status>();

            var result =
                from contract in contracts
                from client in clients
                from service in services
                from status in statuses

                where contract.Worker.Agent.AgentId == agentId

                where contract.Client.ClientId == client.ClientId
                where contract.ServiceId == service.ServiceId
                where contract.Status.StatusId == status.StatusId

                select new
                {
                    contract.ContractId,
                    contract.DateOfConclusion,
                    ServiceName = service.Title,
                    ClientName = client.FullName,
                    contract.Cost,
                    contract.InsurancePeriod,
                    contract.Comment,
                    Status = status.Title
                };

            return result.OrderBy(x => x.ContractId);
        }
        public override List<Contract> GetContractsInProcessingByAgentId(int agentId)
        {
            return adapter.GetAll<Classes.NoSQL.Contract>()
            .Where(x => x.Worker.Agent.AgentId == agentId && x.Status.StatusId == 2)
            .Select(contract => contract.ToSQLContract())
            .ToList();
        }
        public override List<Contract> GetConfirmedContractsByAgentId(int agentId)
        {
            return adapter.GetAll<Classes.NoSQL.Contract>()
            .Where(x => x.Worker.Agent.AgentId == agentId && x.Status.StatusId == 1)
            .Select(contract => contract.ToSQLContract())
            .ToList();
        }


        // by manager
        public override IEnumerable<object> GetReportByManagerId(int managerId)
        {
            var contracts = adapter.GetAll<Classes.NoSQL.Contract>();
            var managers = users.GetManagers();
            var agents = users.GetAgents();
            var workers = users.GetWorkers();
            var clients = users.GetClients();
            var services = adapter.GetAll<Classes.Service>();
            var departaments = filials.GetDepartaments();
            var dTypes = adapter.GetAll<DType>();

            var departamentId = managerService.GetDepartamentIdByManagerId(managerId);

            var agentInfos =
                from agent in agents
                from worker in workers
                where agent.WorkerId == worker.WorkerId
                where worker.DepartamentId == departamentId
                select new
                {
                    agent.AgentId,
                    AgentName = worker.FullName,
                };

            var result =
               (from contract in contracts
                where contract.Status.StatusId == 1
                from agentInfo in agentInfos

                select new
                {
                    AgentId = agentInfo.AgentId,
                    AgentName = agentInfo.AgentName,
                    SumOfContracts =
                       (from agentContract in contracts
                        where agentContract.Worker.Agent.AgentId == agentInfo.AgentId
                        select agentContract.Cost).Sum(),
                    CountOfContracts =
                        (from agentContract in contracts
                         where agentContract.Worker.Agent.AgentId == agentInfo.AgentId
                         select agentContract.Cost).Count()
                }).Distinct();

            return result;
        }
    }
}
