using Mahali.Models;
using Mahali.Repositories.Interfaces;
using Mahali.Services;
using MahaliDtos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahaliTesting.ServicesTesting
{
    public class CategoryServiceTestClass
    {
        private readonly Mock<ICategoryInterface> _categoryInterfaceMock = new Mock<ICategoryInterface>();
        private readonly Mock<IShopInterface> _shopInterface = new Mock<IShopInterface>();
        private readonly CategoryService _categoryService;

        public CategoryServiceTestClass()
        {
            _categoryService = new CategoryService(_categoryInterfaceMock.Object,_shopInterface.Object);
        }

        [Fact]
        public async Task ModifyCategoryNameAsync_ShouldModifyName_WhenCategoryExistsAndNewNameIsUnique()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = Category.Create("Category1");
            category.GetType().GetProperty("Id").SetValue(category, categoryId);
            _categoryInterfaceMock.Setup(x => x.GetByIdAsync(categoryId)).ReturnsAsync(category);
            _categoryInterfaceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Category> { category });
            var parameters = new CategoryUpdateParameters { CategoryId = categoryId, UpdatedName = "UpdatedCategory1" };

            // Act
            await _categoryService.ModifyCategoryNameAsync(parameters);

            // Assert
            _categoryInterfaceMock.Verify(x => x.UpdateAsync(category), Times.Once);
            Assert.Equal("UpdatedCategory1", category.Name);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllCategories_WhenCategoriesExist()
        {
            // Arrange
            var categories = new List<Category>
            {
                Category.Create("Category1"),
                Category.Create("Category2")
            };
            _categoryInterfaceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(categories);

            // Act
            var result = await _categoryService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categories.Count, result.Count);
            Assert.Equal(categories[0].Name, result[0].Name);
            Assert.Equal(categories[1].Name, result[1].Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCategoryWithProducts_WhenCategoryExists()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = Category.Create("Category1");
            category.GetType().GetProperty("Id").SetValue(category, categoryId);
            category.SetProducts(new List<Product>
            {
                Product.Create("Product1", "Description1", 10, 100, "imageUri1", Guid.NewGuid())
            });
            _categoryInterfaceMock.Setup(x => x.GetByIdAsync(categoryId)).ReturnsAsync(category);
            var parameter = new CategoryGetByParameter { CategoryId = categoryId };

            // Act
            var result = await _categoryService.GetByIdAsync(parameter);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(category.Name, result.Name);
            Assert.NotNull(result.Products);
            Assert.Single(result.Products);
            Assert.Equal(category.Products[0].Name, result.Products[0].Name);
            Assert.Equal(category.Products[0].Description, result.Products[0].Description);
            Assert.Equal(category.Products[0].Price, result.Products[0].Price);
        }

        [Fact]
        public async Task GetCategoryProductsAsync_ShouldReturnProducts_WhenCategoryExists()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = Category.Create("Category1");
            category.GetType().GetProperty("Id").SetValue(category, categoryId);
            category.SetProducts(new List<Product>
            {
                Product.Create("Product1", "Description1", 10, 100, "imageUri1", Guid.NewGuid())
            });
            _categoryInterfaceMock.Setup(x => x.GetByIdAsync(categoryId)).ReturnsAsync(category);
            var parameter = new CategoryGetByParameter { CategoryId = categoryId };

            // Act
            var result = await _categoryService.GetCategoryProductsAsync(parameter);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(category.Products[0].Name, result[0].Name);
            Assert.Equal(category.Products[0].Description, result[0].Description);
            Assert.Equal(category.Products[0].Price, result[0].Price);
        }


    }
}
