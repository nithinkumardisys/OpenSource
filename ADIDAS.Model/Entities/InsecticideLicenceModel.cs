//------------------------------------------------------------------------------
// <copyright file="InsecticideLicenceModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace DIDAS.Model.Entities
{
    /// <summary>
    /// InsecticideLicenceModel.
    /// </summary>
    public class InsecticideLicenceModel
    {
        /// <summary>
        /// Gets or Sets FirmName.
        /// </summary>
        public string FirmName { get; set; }

        /// <summary>
        /// Gets or Sets LicenceNo.
        /// </summary>
        public string LicenceNo { get; set; }

        /// <summary>
        /// Gets or Sets LicenceDate.
        /// </summary>
        public string LicenceDate { get; set; }

        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public string Distcode { get; set; }

        /// <summary>
        /// Gets or Sets DistName.
        /// </summary>
        public string DistName { get; set; }
    }
}
