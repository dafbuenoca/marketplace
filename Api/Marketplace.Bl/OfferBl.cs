// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.Core.Bl;
using Marketplace.Core.Dal;
using Marketplace.Core.Model;

namespace Marketplace.Bl;

/// <summary>
///     Offers logic
/// </summary>
/// <seealso cref="Marketplace.Core.Bl.IOfferBl" />
public class OfferBl : IOfferBl
{
    #region Fields

    private readonly IOfferRepository offerRepository;
    private readonly IUserRepository userRepository;

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="OfferBl" /> class.
    /// </summary>
    /// <param name="offerRepository">The user repository.</param>
    public OfferBl(IOfferRepository offerRepository, IUserRepository userRepository)
    {
        this.offerRepository = offerRepository;
        this.userRepository = userRepository;
    }

    #endregion

    #region Methods

    /// <inheritdoc />
    public async Task<IEnumerable<Offer>> GetOffersAsync(int pageNumber, int pageSize)
    {
        return await offerRepository.GetAllOffersAsync(pageNumber, pageSize).ConfigureAwait(false);
    }

    public async Task<int> GetTotalOfferCountAsync()
    {
        return await offerRepository.GetTotalOfferCountAsync().ConfigureAwait(false);
    }

    public async Task<Offer> CreateOffer(Offer offer)
    {
        User user = await userRepository.GetUserByIdAsync(offer.UserId);
        
        if (user == null)
        {
            user = new User { Id = offer.UserId };
            user = await userRepository.CreateUserAsync(user);
        }

        offer.User = user;

        return await offerRepository.CreateOffer(offer);
    }

    #endregion
}