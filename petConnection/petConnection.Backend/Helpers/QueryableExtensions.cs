using System;
using petConnection.Share.DTOs;

namespace petConnection.Backend.Helpers
{
	public static class QueryableExtensions // claese de extensión debe ser estática 
	{
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO pagination)
        {
            return queryable
                .Skip((pagination.Page - 1) * pagination.RecordsNumber)
                .Take(pagination.RecordsNumber);
        }
    }
}