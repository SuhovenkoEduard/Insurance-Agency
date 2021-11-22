using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Classes.NoSQL
{
    public class Filial
    {
		public int FilialId { get; set; }
		public int NumberOfWorkers { get; set; }
		public string City { get; set; }
		public string DateOfCreation { get; set; }
		public Departament[] Departments { get; set; }

		public Filial() { }

		public Filial(
			int numberOfWorkers, 
			string city, 
			string dateOfCreation,
			Departament[] departments)
		{
			NumberOfWorkers = numberOfWorkers;
			City = city;
			DateOfCreation = dateOfCreation;
			Departments = departments;
		}

		public override string ToString()
		{
			return FilialId + " " +
				NumberOfWorkers + " " +
				City + " " +
				DateOfCreation + " " + 
				Departments;
		}

		public Classes.Filial ToSQLFilial()
        {
			var filial = new Classes.Filial(NumberOfWorkers, City, DateOfCreation);
			filial.FilialId = FilialId;
			return filial;
        }
	}
}
