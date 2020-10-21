using System.Threading.Tasks;

namespace Prometyum.Sample.Data
{
    public interface ISampleDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
