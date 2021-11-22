using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Classes
{
	public class Client
	{
		public int ClientId { get; set; }
		public string PhoneNumber { get; set; }
		public string FullName { get; set; }
		public string Adress { get; set; }
		public int UserId { get; set; }

		public Client() { }

		public Client(string phoneNumber, string fullName, string adress, int userId)
		{
			PhoneNumber = phoneNumber;
			FullName = fullName;
			Adress = adress;
			UserId = userId;
		}

		public override string ToString()
		{
			return ClientId + " " +
				PhoneNumber + " " +
				FullName + " " +
				Adress + " " +
				UserId;
		}
	}
}
