// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

namespace Marketplace.Dal.Repositories
{
    using System.Threading.Tasks;
    using Marketplace.Core.Dal;
    using Marketplace.Core.Model;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : IUserRepository
    {
        #region Fields

        private MarketplaceContext context;

        #endregion

        #region Constructors

        public UserRepository()
        {
            this.context = new MarketplaceContext();
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public async Task<User[]> GetAllUsersAsync()
        {
            return await this.context.Users.ToArrayAsync();
        }

        #endregion
    }
}