// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

using Marketplace.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Core.Dal
{
    public interface ICategoryRepository
    {

        #region Methods

        /// <summary>
        ///     Gets all categories asynchronous.
        /// </summary>
        /// <returns>Array of categories</returns>
        Task<Category[]> GetAllCategoriesAsync();

        #endregion
    }
}
