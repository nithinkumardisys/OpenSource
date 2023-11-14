//------------------------------------------------------------------------------
// <copyright file="PanchayatResponse.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    public class PanchayatResponse
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
    }
}
