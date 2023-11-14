//------------------------------------------------------------------------------
// <copyright file="Jwt.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// Jwt.
    /// </summary>
    public class Jwt
    {
        /// <summary>
        /// Gets or Sets Key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or Sets AccessTimeOut.
        /// </summary>
        public int AccessTimeOut { get; set; }

        /// <summary>
        /// Gets or Sets RefershTimeOut.
        /// </summary>
        public int RefershTimeOut { get; set; }
    }
}
