// <copyright file="TokenApiModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// TokenApiModel.
    /// </summary>
    public class TokenApiModel
    {
        /// <summary>
        /// Gets or Sets AccessToken.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or Sets RefreshToken.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
