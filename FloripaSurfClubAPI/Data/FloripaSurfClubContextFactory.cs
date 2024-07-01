using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FloripaSurfClubCore.Data
{
    public class FloripaSurfClubContextFactory : IDesignTimeDbContextFactory<FloripaSurfClubContextV2>
    {
        public FloripaSurfClubContextV2 CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<FloripaSurfClubContextV2>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            return new FloripaSurfClubContextV2(optionsBuilder.Options);
        }
    }
}
