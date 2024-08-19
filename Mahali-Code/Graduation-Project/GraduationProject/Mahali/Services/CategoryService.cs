
using Mahali.Models;
using Mahali.Repositories.Interfaces;
using MahaliDtos;

namespace Mahali.Services
{
    public class CategoryService
    {
        private readonly ICategoryInterface _categoryInterface;
        private readonly IShopInterface _shopInterface;
        public CategoryService(ICategoryInterface categoryInterface , IShopInterface shopInterface)
        {
            _categoryInterface = categoryInterface;
            _shopInterface = shopInterface;
        }

        public async Task AddAsync(CategoryCreateParameters parameters)
        {
            var categories = await _categoryInterface.GetAllAsync();
            foreach(var category in categories) 
            {
                if (category.Name == parameters.Name) return;
                else continue;
            }
            var newCategory = Category.Create(parameters.Name);
            await _categoryInterface.AddAsync(newCategory);
        }

        public async Task DeleteAsync(CategoryDeleteParameters parameters)
        {
            var category = await _categoryInterface.GetByIdAsync(parameters.CategoryId);
            if (category == null) return;
            await _categoryInterface.DeleteAsync(category);
               
        }

        public async Task ModifyCategoryNameAsync(CategoryUpdateParameters parameters)
        {
            var selectedCategory = await _categoryInterface.GetByIdAsync(parameters.CategoryId);
            if (selectedCategory == null) return;

            var categories = await _categoryInterface.GetAllAsync();
            foreach (var category in categories)
            {
                if (category.Name == parameters.UpdatedName) return;
                else continue;
            }
            selectedCategory.SetName(parameters.UpdatedName);
            await _categoryInterface.UpdateAsync(selectedCategory);
        }

        public async Task<List<CategoryListItems>?> GetAllAsync()
        {
            var categories = await _categoryInterface.GetAllAsync();
            if (categories == null) return null;

            
            return categories.Select(c => new CategoryListItems
            {
                ID = c.Id,
                Name = c.Name,
            }).ToList();
        }

        public async Task<CategoryListItems?> GetByIdAsync(CategoryGetByParameter parameter)
        {
            var category = await _categoryInterface.GetByIdAsync(parameter.CategoryId);
            if (category == null) return null;
            
            var products = category.Products.Select( p=> new ProductListItems
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
               
            }).ToList();
            return  new CategoryListItems
            {
               
                Name = category.Name,
                Products = products,
            };
        }

        public async Task<List<ProductListItems>?> GetCategoryProductsAsync(CategoryGetByParameter parameter)
        {
            var category = await _categoryInterface.GetByIdAsync(parameter.CategoryId);
            if (category == null) return null;
            var shops = await _shopInterface.GetAllAsync();
            return category.Products.Select(c => new ProductListItems
            {
                Name = c.Name,
                Description = c.Description,
                Price = c.Price,
                Shop = shops.Select(s => new ShopDetails
                {
                    Id = s.Id,
                    Name = s.Name ,
                    Description = s.Description,
                    Email = s.Email,
                    Password = s.Password
                }).FirstOrDefault(y => y.Id == c.ShopId)
            }).ToList();
        }
    }
}
