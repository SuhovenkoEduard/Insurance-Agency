using CourseApp.DataLayer.Adapters;
using CourseApp.DataLayer.Services;

namespace CourseApp.DataLayer
{
    public class DataLayer
    {
        public AgentService Agents { get; set; }
        public ClientService Clients { get; set; }
        public ContractService Contracts { get; set; }
        public DepartamentService Departaments { get; set; }
        public DTypeService DTypes { get; set; }
        public FilialService Filials { get; set; }
        public ManagerService Managers { get; set; }
        public ServiceService Services { get; set; }
        public UserService Users { get; set; }
        public WorkerService Workers { get; set; }

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
