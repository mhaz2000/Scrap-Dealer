using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Services
{
    internal class SubCategoryReadService : ISubCategoryReadService
    {
        private readonly DbSet<SubCategoryReadModel> _subCategories;

        public SubCategoryReadService(ReadDbContext context)
            => _subCategories = context.SubCategories;

        public async Task<bool> ExistsByNameAsync(string name, Guid? excludedId = null)
            => await _subCategories.AnyAsync(c => c.Name == name && (excludedId != null ? c.Id != excludedId : true));
    }
}
