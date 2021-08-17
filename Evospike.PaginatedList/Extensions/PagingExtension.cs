using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evospike.PaginatedList.Models;
using Microsoft.EntityFrameworkCore;

namespace Evospike.PaginatedList.Extensions
{
    public static class PagingExtension
    {
        public static async Task<DataCollection<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int take)
        {
            var originalPages = page;
            page--;

            if (page > 0)
                page *= take;

            var result = new DataCollection<T>
            {
                Items = await query.Skip(page).Take(take).ToListAsync(),
                TotalItems = await query.CountAsync(),
                Page = originalPages
            };

            if (result.TotalItems > 0)
            {
                result.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.TotalItems) / take));
            }

            return result;
        }

        public static DataCollection<T> GetPaged<T>(this List<T> source, int page, int take)
        {
            var originalPages = page;
            page--;

            if (page > 0)
                page *= take;

            var result = new DataCollection<T>
            {
                Items = source.Skip(page).Take(take).ToList(),
                TotalItems = source.Count,
                Page = originalPages
            };

            if (result.TotalItems > 0)
            {
                result.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.TotalItems) / take));
            }

            return result;
        }
    }
}
