using CourseApp.Classes;
using CourseApp.DataLayer.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Services.NoSQL
{
    public class ClientSer2 : ClientService
    {
        private UserSer2 users;
        public ClientSer2(IAdapter adapter, UserSer2 users)
            : base(adapter) 
        {
            this.users = users;
        }

        public override string GetFullInfoById(int clientId)
        {
            
            var clients = users.GetClients();
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
        public override Client GetClientByUserId(int userId) => users.GetUserById(userId).Client;
        public override void Add(Client client)
        {
            var user = users.GetUserById(client.UserId);
            client.ClientId = users.GetClients().Max(c => c.ClientId) + 1;
            user.Client = client;
            adapter.Update(user);
        }
    }
}
