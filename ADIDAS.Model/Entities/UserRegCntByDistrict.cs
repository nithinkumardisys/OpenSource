//----------------------------------------------------------------------------
// <copyright file="UserRegCntByDistrict.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// UserRegCntByDistrict.
    /// </summary>
    public class UserRegCntByDistrict
    {
        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Designation.
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or Sets Approval_status.
        /// </summary>
        public string Approval_status { get; set; }

        /// <summary>
        /// Gets or Sets TotalUsersCount.
        /// </summary>
        public int TotalUsersCount { get; set; }
    }
}
