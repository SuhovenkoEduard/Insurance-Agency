using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Classes
{
	public class Service
	{
		public int ServiceId { get; set; }
		public string Title { get; set; }
		public int DTypeId { get; set; }
		public int Cost { get; set; }

		public Service() { }

		public Service(string title, int dTypeId, int cost)
		{
			Title = title;
			DTypeId = dTypeId;
			Cost = cost;
		}

		public override string ToString()
		{
			return ServiceId + " " +
				Title + " " +
				DTypeId + " " +
				Cost;
		}
	}
}
