using CourseApp.DataLayer.Adapters;
using CourseApp.Classes.NoSQL;
using System.Linq;
using CourseApp.Classes;
using System.Collections.Generic;

namespace CourseApp.DataLayer.Services.NoSQL
{
    public class UserSer2 : UserService
    {
        public UserSer2(IAdapter adapter)
            : base(adapter) { }

        public override bool Contains(Classes.User user)
        {
            return adapter.GetAll<Classes.User>().Any(x => x.Login == user.Login && x.Password == user.Password);
        }
        public override bool ContainsLogin(string login)
        {
            return adapter.GetAll<Classes.NoSQL.User>().Any(x => x.Login == login);
        }
        public override int GetId(Classes.User user)
        {
            return adapter.GetAll<Classes.NoSQL.User>().First(x => x.Login == user.Login && x.Password == user.Password).UserId;
        }
        public override Role GetRole(int userId)
        {
            List<Classes.NoSQL.User> users = adapter.GetAll<Classes.NoSQL.User>();
            Classes.NoSQL.User user = users
                .Find(u => u.UserId == userId);

            if (user.Worker != null) // __worker
            {
                if (user.Worker.Agent != null)
                    return Role.Agent;                // agent
                else
                    return Role.Manager;              // manager
            }
            else                                    // client
            {
                return Role.Client;
            }
        }
        public override void Add(Classes.User user) 
        {
            Classes.NoSQL.User newUser 
                = new Classes.NoSQL.User
                (user.Login, user.Password, null, null);
            adapter.Add(newUser);
        }

        // additional
        public List<Client> GetClients() => adapter.GetAll<Classes.NoSQL.User>().Where(user => user.Client != null).Select(user => user.Client).ToList();
        public List<Classes.NoSQL.Worker> GetWorkers() => adapter.GetAll<Classes.NoSQL.User>().Where(user => user.Worker != null).Select(user => user.Worker).ToList();
        public List<Agent> GetAgents() => GetWorkers().Where(worker => worker.Agent != null).Select(worker => worker.Agent).ToList();
        public List<Manager> GetManagers() => GetWorkers().Where(worker => worker.Manager != null).Select(worker => worker.Manager).ToList();
        public Classes.NoSQL.User GetUserById(int userId) => adapter.GetAll<Classes.NoSQL.User>().Find(user => user.UserId == userId);
    }
}
