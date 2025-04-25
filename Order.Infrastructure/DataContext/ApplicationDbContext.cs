using Microsoft.EntityFrameworkCore;

namespace Order.Infrastructure.DataContext
{
    public class CommandDbContext : DbContext
    {
        public CommandDbContext(DbContextOptions<CommandDbContext> options) : base(options) { }

        public DbSet<Order.Domain.Models.Order> Orders { get; set; }

    }

    public class QueryDbContext : DbContext
    {
        public QueryDbContext(DbContextOptions<QueryDbContext> options) : base(options) { }

        public DbSet<Order.Domain.Models.Order> Orders { get; set; }

    }
}
