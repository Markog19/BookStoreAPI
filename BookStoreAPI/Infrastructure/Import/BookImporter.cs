using BookStoreAPI.Domain.Interfaces;
using Quartz;
namespace BookStoreAPI.Infrastructure.Import;

public class BookImporter(ILogger<BookImporter> _logger, IServiceProvider _serviceProvider) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Import started");
        try
        {
            using var scope = _serviceProvider.CreateScope();

            var importer = scope.ServiceProvider.GetRequiredService<IBookImport>();

            await importer.ImportAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Import failed.");
        }
        _logger.LogInformation("Import finished");
    }

   
}
