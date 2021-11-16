using CourseApp.Classes;
using CourseApp.DataLayer.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Services
{
    public class WorkerService : Service
    {
        public WorkerService(IAdapter adapter)
            : base(adapter) { }

        public void Update(Worker worker) => adapter.Update(worker);
        public List<Worker> GetWorkersByDepartamentId(int departamentId)
        {
            var workers = adapter.GetAll<Worker>();
            var result =
                from worker in workers
                where worker.DepartamentId == departamentId
                select worker;
            return result.ToList();
        }
        public bool ContainsUserId(int userId)
        {
            return adapter.GetAll<Worker>().Any(x => x.UserId == userId);
        }
        public int GetWorkerIdByUserId(int userId)
        {
            return adapter.GetAll<Worker>().First(x => x.UserId == userId).WorkerId;
        }
    }
}
