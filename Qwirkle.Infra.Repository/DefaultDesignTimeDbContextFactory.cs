using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Qwirkle.Infra.Repository;

public class DefaultDesignTimeDbContextFactory : IDesignTimeDbContextFactory<DefaultDbContext>
{
    public DefaultDbContext CreateDbContext(string[] args)
    {
        // use command dotnet ef Database update --connection "<connectionString>"
        // exemple :   dotnet ef Database update --connection "Server=localhost;Port=3306;Database=qwirkle;Uid=thomas;Pwd=2jp5uieg*M69T;"
        var optionsBuilder = new DbContextOptionsBuilder<DefaultDbContext>();
        Version version;
        ServerType type;
        optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=qwirkle;Uid=thomas;Pwd=2jp5uieg*M69T;", ServerVersion.AutoDetect("Server=localhost;Port=3306;Database=qwirkle;Uid=thomas;Pwd=2jp5uieg*M69T;"));
        return new DefaultDbContext(optionsBuilder.Options);
    }
}
