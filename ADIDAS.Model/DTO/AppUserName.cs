//------------------------------------------------------------------------------
// <copyright file="AppUserName.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// App User Name.
    /// </summary>
    public class AppUserName
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
        /// Gets or Sets First_name.
        /// </summary>
        public string First_name { get; set; }

        /// <summary>
        /// Gets or Sets ApprovalStatus.
        /// </summary>
        public string ApprovalStatus { get; set; }

        /// <summary>
        /// Gets or Sets Phone_num.
        /// </summary>
        public string Phone_num { get; set; }
    }
}
