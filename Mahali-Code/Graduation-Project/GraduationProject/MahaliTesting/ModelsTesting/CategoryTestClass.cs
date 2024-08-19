using Mahali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahaliTesting.ModelsTesting
{
    public class CategoryTestClass
    {
        [Fact]
        public void Create_ValidCategory_ShouldSucceed()
        {
            // Arrange
            var name = "ValidCategory";

            // Act
            var category = Category.Create(name);

            // Assert
            Assert.NotNull(category);
            Assert.Equal(name, category.Name);
        }

        [Fact]
        public void Create_NullName_ShouldThrowArgumentNullException()
        {
            // Arrange
            string name = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Category.Create(name));
        }

        [Fact]
        public void Create_EmptyName_ShouldThrowValidationException()
        {
            // Arrange
            var name = "";

            // Act & Assert
          
            Assert.Throws<ArgumentNullException>(() => Category.Create(name));
        }

        [Fact]
        public void SetName_ValidName_ShouldUpdateName()
        {
            // Arrange
            var category = Category.Create("InitialCategory");
            var newName = "UpdatedCategory";

            // Act
            category.SetName(newName);

            // Assert
            Assert.Equal(newName, category.Name);
        }

        [Fact]
        public void SetProducts_ValidProducts_ShouldUpdateProducts()
        {
            // Arrange
            var category = Category.Create("CategoryWithProducts");
            var products = new List<Product>();

            // Act
            category.SetProducts(products);

            // Assert
            Assert.Equal(products, category.Products);
        }
    }
}