using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Classes
{
	public class Manager
	{
		public int ManagerId { get; set; }
		public int WorkerId { get; set; }
		public string PersonalData { get; set; }

		public Manager() { }

		public Manager(int workerId, string personalData)
		{
			WorkerId = workerId;
			PersonalData = personalData;
		}

		public override string ToString()
		{
			return ManagerId + " " +
				WorkerId + " " +
				PersonalData;
		}
	}
}
