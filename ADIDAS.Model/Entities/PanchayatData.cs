// <copyright file="PanchayatData.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// PanchayatData.
    /// </summary>
    public class PanchayatData
    {
        /// <summary>
        /// Gets or Sets District_lg_code.
        /// </summary>
        public int District_lg_code { get; set; }

        /// <summary>
        /// Gets or Sets Block_lg_code.
        /// </summary>
        public int Block_lg_code { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_lg_code.
        /// </summary>
        public int Panchayat_lg_code { get; set; }

        /// <summary>
        /// Gets or Sets ApplicationID
        public string ApplicationID { get; set; }

        /// <summary>
        /// Gets or Sets RegistrationID.
        /// </summary>
        public string RegistrationID { get; set; }

        /// <summary>
        /// Gets or Sets ApplicationStatus.
        /// </summary>
        public string ApplicationStatus { get; set; }
    }
}
