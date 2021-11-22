using CourseApp.Classes;
using CourseApp.DataLayer.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Services
{
    public class ClientService : Service
    {
        public ClientService(IAdapter adapter)
            : base(adapter) { }

        public virtual string GetFullInfoById(int clientId)
        {
            var clients = adapter.GetAll<Client>();
            var result =
                from client in clients
                where client.ClientId == clientId
                select new
                {
                    client.ClientId,
                    client.FullName,
                    client.PhoneNumber,
                    client.Adress
                };

            return result.First().ToString();
        }
        public virtual Client GetClientByUserId(int userId)
        {
            return adapter.GetAll<Client>().First(x => x.UserId == userId);
        }
        public virtual void Add(Client client)
        {
            adapter.Add(client);
        }
    }
}