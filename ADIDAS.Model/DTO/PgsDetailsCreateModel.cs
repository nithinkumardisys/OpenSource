//------------------------------------------------------------------------------
// <copyright file="PgsDetailsCreateModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// CreatePGSModel.
    /// </summary>
    public class PgsDetailsCreateModel
    {
        /// <summary>
        /// Gets or Sets PgsDetailsCreateModel.
        /// </summary>
        public PgsDetailsCreateModel()
        {
            this.FarmerDetails = new PgsFarmerCreateModel();
            this.FarmDetails = new List<PgsFarmCreateModel>();
            this.CropDetails = new List<PgsCropCreateModel>();
        }

        /// <summary>
        /// Gets or Sets FarmerDetails.
        /// </summary>
        public PgsFarmerCreateModel FarmerDetails { get; set; }

        /// <summary>
        /// Gets or Sets FarmDetails.
        /// </summary>
        public List<PgsFarmCreateModel> FarmDetails { get; set; }

        /// <summary>
        /// Gets or Sets CropDetails.
        /// </summary>
        public List<PgsCropCreateModel> CropDetails { get; set; }
    }
}