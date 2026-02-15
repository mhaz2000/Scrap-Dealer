using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Services
{
    internal class CategoryReadService : ICategoryReadService
    {
        private readonly DbSet<CategoryReadModel> _categories;

        public CategoryReadService(ReadDbContext context)
            => _categories = context.Categories;

        public async Task<bool> ExistsByNameAsync(string name, Guid? excludedId = null)
            => await _categories.AnyAsync(c => c.Name == name && (excludedId != null ? c.Id != excludedId : true));
    }
}
