using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RosaryForToday.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RosaryDbContext>
{
    public RosaryDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RosaryDbContext>();
        optionsBuilder.UseSqlite("Data Source=rosary.db");

        return new RosaryDbContext(optionsBuilder.Options);
    }
}
