//------------------------------------------------------------------------------
// <copyright file="NpopDetailsCreateModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// CreateNpopModel.
    /// </summary>
    public class NpopDetailsCreateModel
    {
        /// <summary>
        /// Gets or Sets NpopDetailsCreateModel.
        /// </summary>
        public NpopDetailsCreateModel()
        {
            this.FarmerDetails = new NpopFarmerCreateModel();
            this.FarmDetails = new List<NpopFarmCreateModel>();
            this.CropDetails = new List<NpopCropCreateModel>();
        }

        /// <summary>
        /// Gets or Sets FarmerDetails.
        /// </summary>
        public NpopFarmerCreateModel FarmerDetails { get; set; }

        /// <summary>
        /// Gets or Sets FarmDetails.
        /// </summary>
        public List<NpopFarmCreateModel> FarmDetails { get; set; }

        /// <summary>
        /// Gets or Sets CropDetails.
        /// </summary>
        public List<NpopCropCreateModel> CropDetails { get; set; }
    }
}
