using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Classes
{
	public class User
	{
		public int UserId { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }

		public User() { }

		public User(string login, string password)
		{
			Login = login;
			Password = password;
		}

		public override string ToString()
		{
			return UserId + " " +
				Login + " " +
				Password;
		}
	}
}
