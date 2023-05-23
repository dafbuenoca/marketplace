using Marketplace.Core.Dal;
using Marketplace.Core.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Bl.Test
{
    [TestFixture]
    internal class OfferBlTest
    {
        [Test]
        public async Task GetOffersAsync_ReturnsOffers()
        {
            // Arrange
            var offerRepositoryMock = new Mock<IOfferRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();

             offerRepositoryMock.Setup(repo => repo.GetAllOffersAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(await GetSampleOffers().ConfigureAwait(false)));

            
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int userId) => new User { Id = userId });

            var offerBl = new OfferBl(offerRepositoryMock.Object, userRepositoryMock.Object);

            // Act
            var result = await offerBl.GetOffersAsync(1, 10);

            // Assert
            Assert.IsNotNull(result);;
        }

        [Test]
        public async Task GetTotalOfferCountAsync_ReturnsTotalCount()
        {
            // Arrange
            var offerRepositoryMock = new Mock<IOfferRepository>();
            offerRepositoryMock.Setup(repo => repo.GetTotalOfferCountAsync())
                .ReturnsAsync(10);

            var userRepositoryMock = new Mock<IUserRepository>();

            var offerBl = new OfferBl(offerRepositoryMock.Object, userRepositoryMock.Object);

            // Act
            var result = await offerBl.GetTotalOfferCountAsync();

            // Assert
            Assert.AreEqual(10, result);
        }

        [Test]
        public async Task CreateOffer_CreatesNewOffer()
        {
            // Arrange
            var offerRepositoryMock = new Mock<IOfferRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();

            offerRepositoryMock.Setup(repo => repo.CreateOffer(It.IsAny<Offer>()))
                .ReturnsAsync((Offer offer) => offer);

            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int userId) => new User { Id = userId });

            var offerBl = new OfferBl(offerRepositoryMock.Object, userRepositoryMock.Object);

            var offer = new Offer { UserId = 1 };

            // Act
            var result = await offerBl.CreateOffer(offer);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.UserId);
            Assert.IsNotNull(result.User);
            Assert.AreEqual(1, result.User.Id);
        }

        private Task<Offer[]> GetSampleOffers()
        {
            var offers = new List<Offer>
            {
                new Offer { Id = Guid.NewGuid(), Title = "Offer 1" },
                new Offer { Id = Guid.NewGuid(), Title = "Offer 2" }
            };
            return Task.FromResult(offers.ToArray());
        }
    }
}
