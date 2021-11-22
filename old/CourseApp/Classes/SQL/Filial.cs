using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Classes
{
	public class Filial
	{
		public int FilialId { get; set; }
		public int NumberOfWorkers { get; set; }
		public string City { get; set; }
		public string DateOfCreation { get; set; }

		public Filial() { }

		public Filial(int numberOfWorkers, string city, string dateOfCreation)
		{
			NumberOfWorkers = numberOfWorkers;
			City = city;
			DateOfCreation = dateOfCreation;
		}

		public override string ToString()
		{
			return FilialId + " " +
				NumberOfWorkers + " " +
				City + " " +
				DateOfCreation;
		}
	}
}
