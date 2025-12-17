using Mediaspot.Domain.Titles;

namespace Mediaspot.Domain.Repositories;

public interface ITitleRepository
{
    Task AddAsync(Title title, CancellationToken ct);
    Task DeleteAsync(Title title, CancellationToken ct);
    Task<Title?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<Title>> GetPaginatedAsync(int page,int pageSize, CancellationToken ct);
    Task<bool> ExistsWithNameAsync(string name, CancellationToken ct);
    Task<int> CountAsync(CancellationToken ct);
}