using Application.Features.Brands.Queries.GetById;
using AutoMapper;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Abstracts;
using MediatR;

namespace Application.Features.Categorys.Queries.GetById;

public class GetByIdCategoryQuery : IRequest<GetByIdCategoryResponse>
{
    public int Id { get; set; }
    public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, GetByIdCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GetByIdCategoryQueryHandler(IMapper mapper, ICategoryRepository CategoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = CategoryRepository;
        }

        public async Task<GetByIdCategoryResponse> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            Category? Category = await _categoryRepository.GetAsync(
                predicate: c => c.Id == request.Id,
                withDeleted: true,
                cancellationToken: cancellationToken);

            GetByIdCategoryResponse response = _mapper.Map<GetByIdCategoryResponse>(Category);

            return response;
        }
    }
}
