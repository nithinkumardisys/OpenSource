//------------------------------------------------------------------------------
// <copyright file="SoilConservationDtbNumberResponse.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Create Soil Conservation Dtb Number Response.
    /// </summary>
    public class SoilConservationDtbNumberResponse
    {
        /// <summary>
        /// Gets or Sets DbtNumber.
        /// </summary>
        public string DbtNumber { get; set; }

        /// <summary>
        /// Gets or Sets Mobile_number.
        /// </summary>
        public string Mobile_number { get; set; }

        /// <summary>
        /// Gets or Sets Name_of_principal_beneficiary.
        /// </summary>
        public string Name_of_principal_beneficiary { get; set; }

        /// <summary>
        /// Gets or Sets Village_id.
        /// </summary>
        public string Village_id { get; set; }

        /// <summary>
        /// Gets or Sets Village_name.
        /// </summary>
        public string Village_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public string Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public string District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public string Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets IsValid.
        /// </summary>
        public bool IsValid { get; set; }
    }
}
