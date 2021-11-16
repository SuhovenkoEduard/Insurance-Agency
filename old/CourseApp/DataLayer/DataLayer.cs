using CourseApp.DataLayer.Adapters;
using CourseApp.DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer
{
    public class DataLayer
    {
        public AgentService Agents{ get; }
        public ClientService Clients { get; }
        public ContractService Contracts { get; }
        public DepartamentService Departaments { get; }
        public DTypeService DTypes { get; }
        public FilialService Filials { get; }
        public ManagerService Managers { get; }
        public ServiceService Services { get; }
        public UserService Users { get; }
        public WorkerService Workers { get; }

        public DataLayer(IAdapter adapter)
        {
            Agents = new AgentService(adapter);
            Clients = new ClientService(adapter);
            Managers = new ManagerService(adapter, Agents);
            Contracts = new ContractService(adapter, Managers);
            Departaments = new DepartamentService(adapter);
            DTypes = new DTypeService(adapter);
            Filials = new FilialService(adapter);
            Services = new ServiceService(adapter);
            Workers = new WorkerService(adapter);
            Users = new UserService(adapter, Agents, Workers);
        }
    }
}
