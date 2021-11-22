using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Classes.NoSQL
{
    public class Worker
    {
		public int WorkerId { get; set; }
		public string FullName { get; set; }
		public int MinSalary { get; set; }
		public int UserId { get; set; }
		public int DepartamentId { get; set; }
		public Agent Agent { get; set; }
		public Manager Manager { get; set; }

		public Worker() { }

		public Worker(
			string fullName, 
			int minSalary, 
			int userId, 
			int departamentId,
			Agent agent,
			Manager manager)
		{
			FullName = fullName;
			MinSalary = minSalary;
			UserId = userId;
			DepartamentId = departamentId;
			Agent = agent;
			Manager = manager;
		}

		public override string ToString()
		{
			return WorkerId + " " +
				FullName + " " +
                MinSalary + " " +
				UserId + " " +
				DepartamentId + " " + 
				Agent + " " + 
				Manager;
		}

		public Classes.Worker ToSQLWorker()
        {
			var worker = new Classes.Worker(FullName, MinSalary, UserId, DepartamentId);
			worker.WorkerId = WorkerId;
			return worker;
        }
	}
}
