namespace SampleSentry.API.Repositories.Category
{
    public interface ICategoryRepository
    {
        Task CreateCategoryAsync(Entities.Category category);
    }
}
