//------------------------------------------------------------------------------
// <copyright file="FarmerInfo.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// FarmerInfo.
    /// </summary>
    public class FarmerInfo
    {
        /// <summary>
        /// Gets or Sets Registration_ID.
        /// </summary>
        public string Registration_ID { get; set; }

        /// <summary>
        /// Gets or Sets FarmerName.
        /// </summary>
        public string FarmerName { get; set; }

        /// <summary>
        /// Gets or Sets Father_Husband_Name.
        /// </summary>
        public string Father_Husband_Name { get; set; }

        /// <summary>
        /// Gets or Sets DistrictName.
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or Sets BlockName.
        /// </summary>
        public string BlockName { get; set; }

        /// <summary>
        /// Gets or Sets PanchayatName.
        /// </summary>
        public string PanchayatName { get; set; }

        /// <summary>
        /// Gets or Sets MobileNumber.
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or Sets VillageName.
        /// </summary>
        public string VillageName { get; set; }
    }
}
