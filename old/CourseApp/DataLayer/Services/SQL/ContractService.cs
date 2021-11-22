using CourseApp.Classes;
using CourseApp.DataLayer.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Services
{
    public class ContractService : Service
    {
        private ManagerService managerService;
        public ContractService(IAdapter adapter, ManagerService managerService)
            : base(adapter) 
        {
            this.managerService = managerService;
        }
        public ContractService(IAdapter adapter) 
            : base(adapter) { }

        public virtual void Add(Contract contract)
        {
            adapter.Add(contract);
        }
        public virtual void Update(Contract contract) => adapter.Update(contract);
        public virtual void Remove(Contract contract) => adapter.Remove(contract);
        public virtual Contract GetContractById(int contractId)
        {
            return adapter.GetAll<Contract>()
                .First(x => x.ContractId == contractId);
        }


        // by client
        public virtual IEnumerable<object> GetFullInfoByClientId(int clientId)
        {
            var contracts = adapter.GetAll<Contract>();
            var agents = adapter.GetAll<Agent>();
            var workers = adapter.GetAll<Worker>();
            var services = adapter.GetAll<Classes.Service>();
            var statuses = adapter.GetAll<Status>();

            var result =
                from contract in contracts
                from agent in agents
                from worker in workers
                from service in services
                from status in statuses

                where agent.WorkerId == worker.WorkerId
                
                where contract.AgentId == agent.AgentId
                where contract.ServiceId == service.ServiceId
                where contract.StatusId == status.StatusId

                where contract.ClientId == clientId

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
        public virtual IEnumerable<int> GetContractIdByClientId(int clientId)
        {
            var contracts = adapter.GetAll<Contract>();

            var result =
                from contract in contracts
                where contract.ClientId == clientId
                orderby contract.ContractId
                select contract.ContractId;

            return result;
        }


        // by agent
        public virtual IEnumerable<object> GetFullInfoByAgentId(int agentId)
        {
            var contracts = adapter.GetAll<Contract>();
            var clients = adapter.GetAll<Client>();
            var services = adapter.GetAll<Classes.Service>();
            var statuses = adapter.GetAll<Status>();

            var result =
                from contract in contracts
                from client in clients
                from service in services
                from status in statuses
                
                where contract.AgentId == agentId

                where contract.ClientId == client.ClientId
                where contract.ServiceId == service.ServiceId
                where contract.StatusId == status.StatusId

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
        public virtual List<Contract> GetContractsInProcessingByAgentId(int agentId)
        {
            return adapter.GetAll<Contract>()
            .Where(x => x.AgentId == agentId && x.StatusId == 2)
            .ToList();
        }
        public virtual List<Contract> GetConfirmedContractsByAgentId(int agentId)
        {
            return adapter.GetAll<Contract>()
            .Where(x => x.AgentId == agentId && x.StatusId == 1)
            .ToList();
        }


        // by manager
        public virtual IEnumerable<object> GetReportByManagerId(int managerId)
        {
            var contracts = adapter.GetAll<Contract>();
            var managers = adapter.GetAll<Manager>();
            var agents = adapter.GetAll<Agent>();
            var workers = adapter.GetAll<Worker>();
            var clients = adapter.GetAll<Client>();
            var services = adapter.GetAll<Classes.Service>();
            var departaments = adapter.GetAll<Departament>();
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
                where contract.StatusId == 1
                from agentInfo in agentInfos

                select new
                {
                    AgentId = agentInfo.AgentId,
                    AgentName = agentInfo.AgentName,
                    SumOfContracts =
                       (from agentContract in contracts
                        where agentContract.AgentId == agentInfo.AgentId
                        select agentContract.Cost).Sum(),
                    CountOfContracts =
                        (from agentContract in contracts
                         where agentContract.AgentId == agentInfo.AgentId
                         select agentContract.Cost).Count()
                }).Distinct();

            return result;
        }
    }
}
