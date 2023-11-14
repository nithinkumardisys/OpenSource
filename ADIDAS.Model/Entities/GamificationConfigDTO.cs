//------------------------------------------------------------------------------
// <copyright file="GamificationConfigDTO.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace DepartmentOfAgriculture.Admin.Models.Models
{
    using System;

    /// <summary>
    /// GamificationConfigDto.
    /// </summary>
    public class GamificationConfigDto
    {
        /// <summary>
        /// Gets or Sets District.
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Gets or Sets Block.
        /// </summary>
        public string Block { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat.
        /// </summary>
        public string Panchayat { get; set; }

        /// <summary>
        /// Gets or Sets UserId.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets Reccreateddate.
        /// </summary>
        public DateTime Reccreateddate { get; set; }

        /// <summary>
        /// Gets or Sets UpdateduserId.
        /// </summary>
        public int UpdateduserId { get; set; }

        /// <summary>
        /// Gets or Sets UpdatedDate.
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
