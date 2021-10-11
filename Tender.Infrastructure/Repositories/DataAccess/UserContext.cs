namespace Tender.Infrastructure.Repositories.DataAccess
{
    using Microsoft.EntityFrameworkCore;

    using Tender.Domain.Entities;

    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
    }
}
