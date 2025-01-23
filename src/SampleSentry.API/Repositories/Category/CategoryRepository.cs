using SampleSentry.API.Context;

namespace SampleSentry.API.Repositories.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationContext _context;

        public CategoryRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateCategoryAsync(Entities.Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
        }
    }
}
