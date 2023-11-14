//------------------------------------------------------------------------------
// <copyright file="VillageResponse.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// VillageResponse.
    /// </summary>
    public class VillageResponse
    {
        /// <summary>
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or Sets District.
        /// </summary>
        public DtoLgDistrict District { get; set; }

        /// <summary>
        /// Gets or Sets Block.
        /// </summary>
        public DtoLgBlock Block { get; set; }

        /// <summary>
        /// Gets or Sets Panchayats.
        /// </summary>
        public List<DtoLgPanchayat> Panchayats { get; set; }

        /// <summary>
        /// Gets or Sets Villages.
        /// </summary>
        public List<DtoLgVillage> Villages { get; set; }
    }
}
