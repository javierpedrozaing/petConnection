using System;
using petConnection.Backend.Repositories.Implementations;
using petConnection.Backend.Repositories.Interfaces;
using petConnection.Backend.UnitOfWork.Interfaces;
using petConnection.FrontEnd.Shared.Responses;
using petConnection.Share.DTOs;
using petConnection.Share.Entitties;

namespace petConnection.Backend.UnitOfWork.Implementations
{
	public class ArticlesUnitOfWork : GenericUnitOfWork<Article>, IArticlesUnitOfWork
	{
        private readonly IArticlesRepository _articleRepository;

        public ArticlesUnitOfWork(IGenericRespository<Article> repository, IArticlesRepository articleRepository) : base(repository)
        {
            _articleRepository = articleRepository;
        }

        public override async Task<ActionResponse<IEnumerable<Article>>> GetAsync(PaginationDTO pagination) => await _articleRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _articleRepository.GetTotalPagesAsync(pagination);
    }
}

