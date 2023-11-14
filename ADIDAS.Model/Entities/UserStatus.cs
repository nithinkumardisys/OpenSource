// <copyright file="UserStatus.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// UserStatus.
    /// </summary>
    public class UserStatus
    {
        /// <summary>
        /// Gets or Sets User_id.
        /// </summary>
        public int User_id { get; set; }

        /// <summary>
        /// Gets or Sets User_name.
        /// </summary>
        public string User_name { get; set; }

        /// <summary>
        /// Gets or Sets Approval_status.
        /// </summary>
        public string Approval_status { get; set; }
    }
}
