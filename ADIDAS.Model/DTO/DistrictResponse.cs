//------------------------------------------------------------------------------
// <copyright file="DistrictResponse.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// District Response.
    /// </summary>
    public class DistrictResponse
    {
        /// <summary>
        /// Gets or Sets DistrictResponse.
        /// </summary>
        public DistrictResponse()
        {
            this.Districts = new List<DtoLgDistrict>();
        }

        /// <summary>
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or Sets Districts.
        /// </summary>
        public List<DtoLgDistrict> Districts { get; set; }
    }
}
