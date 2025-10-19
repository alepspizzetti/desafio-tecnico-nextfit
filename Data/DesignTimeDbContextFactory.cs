using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace agendadorAulas.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDb>
    {
        public AppDb CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDb>();
            optionsBuilder.UseSqlite("Data Source=agendador.db");
            return new AppDb(optionsBuilder.Options);
        }
    }
}
