using Mediaspot.Application.Common;
using Mediaspot.Domain.Transcoding;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mediaspot.Worker;

public sealed class TranscodingWorker(ITranscodeJobRepository repo, IUnitOfWork uow, ILogger<TranscodingWorker> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var jobs = await repo.GetPendingAsync(take: 5, stoppingToken);

            foreach (var job in jobs)
            {
                try
                {
                    logger.LogInformation("Starting job {JobId}", job.Id);

                    job.Start();
                    await uow.SaveChangesAsync(stoppingToken);

                    await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);

                    job.Complete();
                    await uow.SaveChangesAsync(stoppingToken);

                    logger.LogInformation("Job {JobId} completed", job.Id);
                }
                catch (Exception ex)
                {
                    job.Fail(ex.Message);
                    await uow.SaveChangesAsync(stoppingToken);

                    logger.LogError(ex, "Job {JobId} failed", job.Id);
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}
