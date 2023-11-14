//------------------------------------------------------------------------------
// <copyright file="LgDirectoryVillageDim.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// LgDirectoryVillageDim.
    /// </summary>
    public partial class LgDirectoryVillageDim
    {

        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int? District_Id { get; set; }

        /// <summary>
        /// Gets or Sets District_Name.
        /// </summary>
        public string District_Name { get; set; }

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
        /// Gets or Sets Village_Id.
        /// </summary>
        public int? Village_Id { get; set; }

        /// <summary>
        /// Gets or Sets Village_Name.
        /// </summary>
        public string Village_Name { get; set; }

        /// <summary>
        /// Gets or Sets user_id.
        /// </summary>
        public int User_id { get; set; }
    }
}
