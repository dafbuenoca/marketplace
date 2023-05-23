// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.Core.Model;

namespace Marketplace.Core.Bl;

/// <summary>
///     Contract for the category logic
/// </summary>
public interface ICategoryBl
{
    #region Methods

    /// <summary>
    ///     Gets the categories.
    /// </summary>
    /// <returns>LIst of categories</returns>
    Task<IEnumerable<Category>> GetCategoriesAsync();

    #endregion
}