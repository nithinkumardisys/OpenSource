//------------------------------------------------------------------------------
// <copyright file="BlockPanchayatByDistrict.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Block Panchayat By District.
    /// </summary>
    public class BlockPanchayatByDistrict
    {
        /// <summary>
        /// Gets or Sets District_lg_code.
        /// </summary>
        public int District_lg_code { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Blocks.
        /// </summary>
        public List<Blocks> Blocks { get; set; }
    }

    /// <summary>
    /// Block Panchayat By Districts.
    /// </summary>
    public class BlockPanchayatByDistricts
    {
        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }
    }

    /// <summary>
    /// Blocks.
    /// </summary>
    public class Blocks
    {
        /// <summary>
        /// Gets or Sets Block_lg_code.
        /// </summary>
        public int Block_lg_code { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat.
        /// </summary>
        public List<Panchayats> Panchayat { get; set; }
    }

    /// <summary>
    /// Panchayats.
    /// </summary>
    public class Panchayats
    {
        /// <summary>
        /// Gets or Sets Panchayat_lg_code.
        /// </summary>
        public int Panchayat_lg_code { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }
    }
}
