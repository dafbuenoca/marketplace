
using Marketplace.Core.Model;
using Marketplace.Dal.Repositories;
using Moq;


namespace Marketplace.Dal.Test.Repositories
{
    [TestFixture]
    public class CategoryRepositoryTest
    {
        [Test]
        public async Task GetAllCategoriesAsync_ReturnsCategories()
        {
            // Arrange
            var contextMock = new Mock<MarketplaceDb>();
            contextMock.Setup(c => c.GetCategoriesAsync()).
                Returns(Task.FromResult(await GetSampleCategories().ConfigureAwait(false)));

            var repository = new CategoryRepository(contextMock.Object);

            // Act
            var result = await repository.GetAllCategoriesAsync();

            // Assert
            Assert.IsNotNull(result);
        }

        private Task<Category[]> GetSampleCategories()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category 1" },
                new Category { Id = 2, Name = "Category 2" }
            };

            return Task.FromResult(categories.ToArray());
        }
    }
}