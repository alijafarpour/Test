using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Mc2.CrudTest.Application.Common.Interfaces;
using Mc2.CrudTest.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            return await base.SaveChangesAsync(cancellationToken);
        }
    }

}
