using CourseApp.Classes;
using CourseApp.DataLayer.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Services
{
    public class UserService : Service
    {
        protected AgentService agentService;
        protected WorkerService workerService;
        public UserService(IAdapter adapter, AgentService agentService, WorkerService workerService)
            : base(adapter) 
        {
            this.agentService = agentService;
            this.workerService = workerService;
        }

        public bool Contains(User user)
        {
            return adapter.GetAll<User>().Any(x => x.Login == user.Login && x.Password == user.Password);
        }
        public bool ContainsLogin(string login)
        {
            return adapter.GetAll<User>().Any(x => x.Login == login);
        }
        public int GetId(User user)
        {
            return adapter.GetAll<User>().First(x => x.Login == user.Login && x.Password == user.Password).UserId;
        }
        public Role GetRole(int userId)
        {
            if (workerService.ContainsUserId(userId)) // __worker
            {
                int workerId = workerService.GetWorkerIdByUserId(userId);
                if (agentService.ContainsWorkerId(workerId))
                    return Role.Agent;                // agent
                else
                    return Role.Manager;              // manager
            } else                                    // client
            {
                return Role.Client;
            }
        }
        public void Add(User user) => adapter.Add(user);
    }
}
