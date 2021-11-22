using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Classes.NoSQL
{
    public class User
    {
		public int UserId { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public Client Client { get; set; }
		public Worker Worker { get; set; }


		public User() { }

		public User(string login, string password, Client client, Worker worker)
		{
			Login = login;
			Password = password;
			Client = client;
			Worker = worker;
		}

		public override string ToString()
		{
			return UserId + " " +
				Login + " " +
				Password + " " +
				Client + " " +
                Worker;
		}
	}
}
