//----------------------------------------------------------------------------
// <copyright file="LoginModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// LoginModel.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or Sets UserName.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or Sets Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets AppregId.
        /// </summary>
        public string AppregId { get; set; }
    }
}
