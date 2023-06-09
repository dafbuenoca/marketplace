// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

using System.Threading.Tasks;
using Marketplace.Core.Dal;
using Marketplace.Core.Model;

namespace Marketplace.Dal.Repositories;

public class UserRepository : IUserRepository
{
    #region Fields

    private readonly MarketplaceDb _context;

    #endregion

    #region Constructors

    public UserRepository()
    {
        _context = new MarketplaceDb();
    }

    #endregion

    #region Methods

    /// <inheritdoc />
    public async Task<User[]> GetAllUsersAsync()
    {
        return await _context.GetUsersAsync();
    }

    public async Task<User> GetUserByIdAsync(int userId) { 
    
        return await _context.GetUserByIdAsync(userId);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        return await _context.CreateUserAsync(user);
    }

    #endregion
}