using Microsoft.EntityFrameworkCore;

namespace ProLearning.Api.Shared.Pagination;

public class PagedList<T>
{
    private PagedList(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public List<T> Items { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> queryable, int page, int pageSize)
    {
        int totalCount = await queryable.CountAsync();
        List<T> items = await queryable.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        
        return new PagedList<T>(items, page, pageSize, totalCount);
    }
}