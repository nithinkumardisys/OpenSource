// <copyright file="TokenData.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// TokenData.
    /// </summary>
    public class TokenData
    {
        public string Refresh_token { get; set; }

        public DateTime Token_expiry_ts { get; set; }
    }
}
