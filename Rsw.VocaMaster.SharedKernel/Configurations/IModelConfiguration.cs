using Microsoft.EntityFrameworkCore;

namespace Rsw.VocaMaster.SharedKernel.Configurations;

public interface IModelConfiguration
{
    void ConfigureModel(ModelBuilder modelBuilder);
}
