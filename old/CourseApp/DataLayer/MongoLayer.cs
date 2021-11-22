using CourseApp.DataLayer.Adapters;
using CourseApp.DataLayer.Services.NoSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer
{
    public class MongoLayer : DataLayer
    {
        public MongoLayer(IAdapter adapter)
            : base(adapter)
        {
            Filials = new FilialSer2(adapter);
            Departaments = new DepartamentSer2(adapter, (FilialSer2) Filials);

            Users = new UserSer2(adapter);
            Clients = new ClientSer2(adapter, (UserSer2) Users);
            Workers = new WorkerSer2(adapter, (UserSer2) Users);
            Agents = new AgentSer2(adapter, (UserSer2) Users, (FilialSer2) Filials);
            Managers = new ManagerSer2(adapter, (UserSer2) Users, (FilialSer2) Filials, (AgentSer2) Agents);

            Contracts = new ContractSer2(adapter, (UserSer2)Users, (FilialSer2)Filials, (ManagerSer2) Managers); // [-]

            Services = new ServiceSer2(adapter, (UserSer2)Users, (FilialSer2) Filials);

        }
    }
}
