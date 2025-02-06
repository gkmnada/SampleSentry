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

        public async Task<Entities.Category> GetCategoryById(Guid id)
        {
            var values = await _context.Categories.FindAsync(id) ?? throw new Exception("Category not found");
            return values;
        }

        public async Task UpdateCategoryAsync(Entities.Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
