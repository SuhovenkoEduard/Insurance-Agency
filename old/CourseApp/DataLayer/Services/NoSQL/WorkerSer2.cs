using CourseApp.Classes;
using CourseApp.DataLayer.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataLayer.Services.NoSQL
{
    public class WorkerSer2 : WorkerService
    {
        protected UserSer2 users;
        public WorkerSer2(IAdapter adapter, UserSer2 users) 
            : base(adapter) 
        {
            this.users = users;
        }

        public override void Update(Worker worker) 
        {
            var workers = users.GetWorkers();
            var cur_worker = workers.Find(w => w.WorkerId == worker.WorkerId);
            cur_worker.FullName = worker.FullName;
            cur_worker.MinSalary = worker.MinSalary;
            adapter.Update(cur_worker);
        }
        public override List<Worker> GetWorkersByDepartamentId(int departamentId)
        {
            var workers = users.GetWorkers();
            var result =
                from worker in workers
                where worker.DepartamentId == departamentId
                select new Worker(worker.FullName, worker.MinSalary, worker.UserId, worker.DepartamentId);
            return result.ToList();
        }
        public override bool ContainsUserId(int userId) => users.GetWorkers().Any(x => x.UserId == userId);
        public override int GetWorkerIdByUserId(int userId) => users.GetWorkers().First(x => x.UserId == userId).WorkerId;
    }
}
