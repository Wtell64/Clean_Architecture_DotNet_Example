using Microsoft.EntityFrameworkCore;

namespace Sm.Crm.Application.Common.Models;

public class PaginatedResult<T>
{
    public IReadOnlyCollection<T>? Items { get; init; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }

    // Computed Properties
    public bool HasPreviousPage => Page > 1;

    public bool HasNextPage => Page < TotalPages;
    public int OffsetFrom => (Page - 1) * PageSize + 1;
    public int OffsetTo => Math.Min(Page * PageSize, TotalItems);

    public PaginatedResult(IReadOnlyCollection<T> items, int count, int page, int pageSize)
    {
        Items = items;
        TotalItems = count;
        Page = page;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(TotalItems / (double)PageSize);
    }

    public static async Task<PaginatedResult<T>> Create(IQueryable<T> source, int page, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedResult<T>(items, count, page, pageSize);
    }
}