// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright
using Marketplace.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Core.Bl
{
    /// <summary>
    ///     Contract for the Offer logic
    /// </summary>
    public interface IOfferBl
    {

        #region Methods

        /// <summary>
        ///     Gets the offers.
        /// </summary>
        /// <returns>LIst of offers</returns>
        Task<IEnumerable<Offer>> GetOffersAsync(int pageNumber, int pageSize);

        /// <summary>
        ///     Gets the total of the offers.
        /// </summary>
        /// <returns>LIst of offers</returns>
        Task<int> GetTotalOfferCountAsync();

        Task<Offer> CreateOffer(Offer offer);

        #endregion
    }
}
