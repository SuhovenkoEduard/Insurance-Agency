using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Classes.NoSQL
{
    public class Contract
    {
		public int ContractId { get; set; }
		public string DateOfConclusion { get; set; }
		public int ServiceId { get; set; }
		public Client Client { get; set; }
		public Worker Worker { get; set; }
		public int Cost { get; set; }
		public int InsurancePeriod { get; set; }
		public string Comment { get; set; }
		public Status Status { get; set; }

		public Contract() { }

		public Contract(
			string dateOfConclusion, 
			int serviceId, 
			Client client, 
			Worker worker, 
			int cost, 
			int insurancePeriod, 
			string comment, 
			Status status)
		{
			DateOfConclusion = dateOfConclusion;
			ServiceId = serviceId;
			Client = client;
			Worker = worker;
			Cost = cost;
			InsurancePeriod = insurancePeriod;
			Comment = comment;
			Status = status;
		}

		public override string ToString()
		{
			return ContractId + " " +
				DateOfConclusion + " " +
				ServiceId + " " +
				Client + " " +
				Worker + " " +
				Cost + " " +
				InsurancePeriod + " " +
				Comment + " " +
				Status;
		}

		public Classes.Contract ToSQLContract()
        {
			return new Classes.Contract(
					this.DateOfConclusion,
					this.ServiceId,
					this.Client.ClientId,
					this.Worker.Agent.AgentId,
					this.Cost,
					this.InsurancePeriod,
					this.Comment,
					this.Status.StatusId);
		}
	}
}
