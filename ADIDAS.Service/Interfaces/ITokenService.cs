//------------------------------------------------------------------------------
// <copyright file="ITokenService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Service.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text;

    /// <summary>
    /// ITokenService.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// GenerateAccessToken.
        /// </summary>
        /// <param name="claims">claims.</param>
        /// <returns>string.</returns>
        string GenerateAccessToken(IEnumerable<Claim> claims);

        /// <summary>
        /// GenerateRefreshToken.
        /// </summary>
        /// <returns>string.</returns>
        string GenerateRefreshToken();

        /// <summary>
        /// GetPrincipalFromExpiredToken.
        /// </summary>
        /// <param name="token">token.</param>
        /// <returns>ClaimsPrincipal.</returns>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
