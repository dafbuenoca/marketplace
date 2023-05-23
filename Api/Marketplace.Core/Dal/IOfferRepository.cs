// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>
using Marketplace.Core.Model;
using System.Threading.Tasks;

namespace Marketplace.Core.Dal
{
    /// <summary>
    ///     Contract for the Offer data access
    /// </summary>
    public interface IOfferRepository
    {

        #region Methods

        /// <summary>
        ///     Gets all offers asynchronous.
        /// </summary>
        /// <returns>Array of offers</returns>
        Task<Offer[]> GetAllOffersAsync(int pageNumber, int pageSize);

        /// <summary>
        ///     Gets th total offers asynchronous.
        /// </summary>
        /// <returns>Array of offers</returns>
        Task<int> GetTotalOfferCountAsync();

        Task<Offer> CreateOffer(Offer offer);

        #endregion
    }
}
