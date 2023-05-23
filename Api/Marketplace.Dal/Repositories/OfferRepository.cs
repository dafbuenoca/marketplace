// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>
using Marketplace.Core.Dal;
using Marketplace.Core.Model;
using System.Threading.Tasks;

namespace Marketplace.Dal.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        #region Fields

        private readonly MarketplaceDb _context;

        #endregion

        #region Constructors

        public OfferRepository()
        {
            _context = new MarketplaceDb();
        }

        #endregion

        #region Methods

        public async Task<Offer[]> GetAllOffersAsync(int pageNumber, int pageSize)
        {
            return await _context.GetOffersAsync(pageNumber, pageSize);
        }

        public async Task<int> GetTotalOfferCountAsync()
        {
            return await _context.GetTotalOfferCountAsync();

        }

        public async Task<Offer> CreateOffer(Offer offer)
        {
            return await _context.CreateOfferAsync(offer);
        }

        #endregion


    }
}
