// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

namespace Marketplace.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Marketplace.Core.Bl;
    using Marketplace.Core.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Services for offers
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    public class OfferController : ControllerBase
    {
        #region Fields

        private readonly ILogger<OfferController> logger;

        private readonly IOfferBl offerBl;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="offerBl">The user business logic.</param>
        public OfferController(ILogger<OfferController> logger, IOfferBl offerBl)
        {
            this.logger = logger;
            this.offerBl = offerBl;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the list of offers.
        /// </summary>
        /// <returns>List of offers</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagedList<Offer>>>> Get(int pageIndex = 1, int pageSize = 10)
        {
            IEnumerable<Offer> result;
            PagedList<Offer> pagedList;

            try
            {
                result = await this.offerBl.GetOffersAsync(pageIndex, pageSize).ConfigureAwait(false);
                int totalOffers = await this.offerBl.GetTotalOfferCountAsync().ConfigureAwait(false);
                List<Offer> resultList = new List<Offer>(result);
                pagedList = new PagedList<Offer>(resultList, totalOffers, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server Error.");
            }

            return this.Ok(pagedList);
        }

        [HttpPost]
        public async Task<ActionResult<Offer>> Post([FromBody] OfferRequest offerRequest)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                Offer offer = new Offer
                {
                    Title = offerRequest.Title,
                    Description = offerRequest.Description,
                    Location = offerRequest.Location,
                    PictureUrl = offerRequest.PictureUrl,
                    CategoryId = offerRequest.CategoryId,
                    UserId = offerRequest.UserId
                };

                offer = await this.offerBl.CreateOffer(offer).ConfigureAwait(false);

                return this.Ok(offer);
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server Error.");
            }

        }

        #endregion
    }
}