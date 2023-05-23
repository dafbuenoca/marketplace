using Marketplace.Core.Dal;
using Marketplace.Core.Model;
using Moq;

namespace Marketplace.Bl.Test
{
    [TestFixture]
    internal class CategoryBlTest
    {

        [Test]
        public async Task GetCategoriesAsync_ReturnsCategories()
        {
            // Arrange
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            
            categoryRepositoryMock.Setup(repo => repo.GetAllCategoriesAsync())
            .Returns(Task.FromResult(await GetSampleCategories().ConfigureAwait(false)));

            var categoryBl = new CategoryBl(categoryRepositoryMock.Object);

            // Act
            var result = await categoryBl.GetCategoriesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        private Task<Category[]> GetSampleCategories()
        {
            var categories = new List<Category>();


            var category = new Category { Id = 1, Name = "category1" };
            var category2 = new Category { Id = 2, Name = "category2" };

            categories.Add(category);
            categories.Add(category2);

            return Task.FromResult(categories.ToArray());
        }
    }
}

