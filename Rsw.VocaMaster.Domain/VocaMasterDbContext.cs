using Microsoft.EntityFrameworkCore;
using Rsw.VocaMaster.SharedKernel.Configurations;

namespace Rsw.VocaMaster.Domain;

public class VocaMasterDbContext : DbContext
{
    private readonly IModelConfiguration _modelConfiguration;

    public VocaMasterDbContext(
        DbContextOptions<VocaMasterDbContext> options, 
        IModelConfiguration modelConfiguration) 
        : base(options)
    {
        _modelConfiguration = modelConfiguration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _modelConfiguration.ConfigureModel(modelBuilder);
    }
}
