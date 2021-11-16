using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Classes
{
	public class Worker
	{
		public int WorkerId { get; set; }
		public string FullName { get; set; }
		public int MinSalary { get; set; }
		public int UserId { get; set; }
		public int DepartamentId { get; set; }

		public Worker() { }

		public Worker(string fullName, int minSalary, int userId, int departamentId)
		{
			FullName = fullName;
			MinSalary = minSalary;
			UserId = userId;
			DepartamentId = departamentId;
		}

		public override string ToString()
		{
			return WorkerId + " " +
				FullName + " " +
				MinSalary + " " +
				UserId + " " +
				DepartamentId;
		}
	}
}
