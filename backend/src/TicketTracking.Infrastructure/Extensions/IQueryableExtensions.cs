using Microsoft.EntityFrameworkCore;
using TicketTracking.Domain.Pagination;

namespace TicketTracking.Infrastructure.Extensions;
public static class IQueryableExtensions
{
    public static async Task<PagedCollection<T>> ToPagedResponseAsync<T>(
        this IQueryable<T> source,
        int pageNumber,
        int pageSize)
    {
        var totalCount = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedCollection<T>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }
}
