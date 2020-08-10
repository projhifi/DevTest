using System.Linq;
using DeveloperTest.Business.Interfaces;
using DeveloperTest.Database;
using DeveloperTest.Database.Models;
using DeveloperTest.Models;
using Microsoft.EntityFrameworkCore;

namespace DeveloperTest.Business
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext context;

        public JobService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public JobModel[] GetJobs()
        {
            return context.Jobs.Include(x => x.Customer).Select(x => new JobModel
            {
                JobId = x.JobId,
                Engineer = x.Engineer,
                When = x.When,
                Customer = new CustomerModel
                {
                    CustomerId = x.Customer == null ? 0 : x.Customer.CustomerId,
                    CustomerName = x.Customer == null ? null : x.Customer.CustomerName,
                    CustomerType = x.Customer == null ? null : x.Customer.CustomerType
                }
            }).ToArray();
        }

        public JobModel GetJob(int jobId)
        {
            return context.Jobs.Include(x => x.Customer).Where(x => x.JobId == jobId).Select(x => new JobModel
            {
                JobId = x.JobId,
                Engineer = x.Engineer,
                When = x.When,
                Customer = new CustomerModel
                {
                    CustomerId = x.Customer == null ? 0 : x.Customer.CustomerId,
                    CustomerName = x.Customer == null ? null : x.Customer.CustomerName,
                    CustomerType = x.Customer == null ? null : x.Customer.CustomerType
                }
            }).SingleOrDefault();
        }

        public JobModel CreateJob(BaseJobModel model)
        {
            var addedJob = context.Jobs.Add(new Job
            {
                Engineer = model.Engineer,
                When = model.When,
                CustomerId = model.Customer.CustomerId
            });

            context.SaveChanges();

            return new JobModel
            {
                JobId = addedJob.Entity.JobId,
                Engineer = addedJob.Entity.Engineer,
                When = addedJob.Entity.When
            };
        }
    }
}
