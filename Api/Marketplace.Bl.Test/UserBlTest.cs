using Marketplace.Core.Dal;
using Marketplace.Core.Model;
using Moq;

namespace Marketplace.Bl.Test
{

    [TestFixture]
    public class UserBlTests
    {
        [Test]
        public async Task GetUsersAsync_ReturnsUsers()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetAllUsersAsync())
    .           Returns(Task.FromResult(await GetSampleUsers().ConfigureAwait(false)));

            var userBl = new UserBl(userRepositoryMock.Object);

            // Act
            var result = await userBl.GetUsersAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count()); 
        }

        private Task<User[]> GetSampleUsers()
        {
            var users = new List<User>();

            var user = new User { Id = 1, Username = "user1" };
            var user2 = new User { Id = 2, Username = "user2" };
            
            users.Add(user);
            users.Add(user2);

            return Task.FromResult(users.ToArray());
        }
    }

}
