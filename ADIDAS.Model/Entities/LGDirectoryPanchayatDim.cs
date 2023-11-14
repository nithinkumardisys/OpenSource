//------------------------------------------------------------------------------
// <copyright file="LGDirectoryPanchayatDim.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// LgDirectoryPanchayatDim.
    /// </summary>
    public partial class LgDirectoryPanchayatDim
    {
        /// <summary>
        /// Gets or Sets Division_Id.
        /// </summary>
        public int? Division_Id { get; set; }

        /// <summary>
        /// Gets or Sets Division_Name.
        /// </summary>
        public string Division_Name { get; set; }

        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int? District_Id { get; set; }

        /// <summary>
        /// Gets or Sets District_Name.
        /// </summary>
        public string District_Name { get; set; }

        /// <summary>
        /// Gets or Sets Sub_Division_Id.
        /// </summary>
        public int? Sub_Division_Id { get; set; }

        /// <summary>
        /// Gets or Sets Sub_Division_Name.
        /// </summary>
        public string Sub_Division_Name { get; set; }

        /// <summary>
        /// Gets or Sets Block_Id.
        /// </summary>
        public int? Block_Id { get; set; }

        /// <summary>
        /// Gets or Sets Block_Name.
        /// </summary>
        public string Block_Name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Id.
        /// </summary>
        public int? Panchayat_Id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Name.
        /// </summary>
        public string Panchayat_Name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Agg_Flag.
        /// </summary>
        public string Panchayat_Agg_Flag { get; set; }
    }
}
