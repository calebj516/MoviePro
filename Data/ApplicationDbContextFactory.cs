using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using MoviePro.Services;
using Microsoft.Extensions.Configuration;

namespace MoviePro.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContextFactory()
        {
        }
        public IConfiguration configuration { get; }

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            //var connectionString = "Server=localhost; Port=5432; Database=MoviePro; User Id=postgres; Password=LearnToCodeNow!";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(
                    ConnectionService.GetConnectionString(configuration));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
