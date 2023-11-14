//------------------------------------------------------------------------------
// <copyright file="DbtDistrictWrapper.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// DistrictWrapper.
    /// </summary>
    public class DbtDistrictWrapper
    {
        /// <summary>
        /// Gets or Sets NotificationUrl.
        /// </summary>
        public DbtDistrictWrapper()
        {
            this.Blocks = new List<DbtBlock>();
        }

        /// <summary>
        /// Gets or Sets NotificationUrl.
        /// </summary>
        public int District_lg_code { get; set; }

        /// <summary>
        /// Gets or Sets NotificationUrl.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets NotificationUrl.
        /// </summary>
        public List<DbtBlock> Blocks { get; set; }
    }

    /// <summary>
    /// DbtBlock.
    /// </summary>
    public class DbtBlock
    {
        /// <summary>
        /// Gets or Sets DbtBlock.
        /// </summary>
        public DbtBlock()
        {
            this.Panchayat = new List<DbtPanchayat>();
        }

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
        public List<DbtPanchayat> Panchayat { get; set; }
    }

    /// <summary>
    /// DbtPanchayat.
    /// </summary>
    public class DbtPanchayat
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

    /// <summary>
    /// DbtBlkPanch.
    /// </summary>
    public class DbtBlkPanch
    {
        /// <summary>
        /// Gets or Sets Block_lg_code.
        /// </summary>
        public int Block_lg_code { get; set; }

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
