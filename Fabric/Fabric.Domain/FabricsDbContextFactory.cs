using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Fabrics.Domain
{
    public class FabricsDbContextFactory : IDesignTimeDbContextFactory<FabricsDbContext>
    {
        public FabricsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FabricsDbContext>();
            optionsBuilder.UseMySQL("Server=localhost;Database=fabric;User=root;Password=vbhflf555;");

            return new FabricsDbContext(optionsBuilder.Options);
        }
    }
}