namespace SampleSentry.API.Repositories.Category
{
    public interface ICategoryRepository
    {
        Task CreateCategoryAsync(Entities.Category category);
        Task<Entities.Category> GetCategoryById(Guid id);
        Task UpdateCategoryAsync(Entities.Category category);
    }
}
