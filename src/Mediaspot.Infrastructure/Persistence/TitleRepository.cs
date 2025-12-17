using Mediaspot.Domain.Repositories;
using Mediaspot.Domain.Titles;
using Microsoft.EntityFrameworkCore;

namespace Mediaspot.Infrastructure.Persistence;

public sealed class TitleRepository(MediaspotDbContext db) : ITitleRepository
{
    public Task<Title?> GetByIdAsync(Guid id, CancellationToken ct)
        => db.Titles.FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task AddAsync(Title title, CancellationToken ct)
        => await db.Titles.AddAsync(title, ct);

    public Task DeleteAsync(Title title, CancellationToken ct)
    {
        db.Titles.Remove(title);
        return Task.CompletedTask;
    }

    public async Task<IReadOnlyList<Title>> GetPaginatedAsync(int page, int pageSize, CancellationToken ct)
    {
        return await db.Titles
            .OrderBy(t => t.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }

    public Task<bool> ExistsWithNameAsync(string name, CancellationToken ct)
        => db.Titles.AnyAsync(t => t.Name == name, ct);

    public Task<int> CountAsync(CancellationToken ct)
        => db.Titles.CountAsync(ct);
}
