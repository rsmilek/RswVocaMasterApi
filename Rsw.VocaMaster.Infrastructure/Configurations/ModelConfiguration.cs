using Microsoft.EntityFrameworkCore;
using Rsw.VocaMaster.SharedKernel.Configurations;

namespace Rsw.VocaMaster.Infrastructure.Configurations
{
    public class ModelConfiguration : IModelConfiguration
    {
        public void ConfigureModel(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAutoscaleThroughput(1000);

            modelBuilder.HasDefaultContainer("UserVocabularies");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ModelConfiguration).Assembly);
        }
    }
}
