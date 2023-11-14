// <copyright file="PestSurveillanceDisease.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// PestSurveillanceDisease.
    /// </summary>
    public class PestSurveillanceDisease
    {
        /// <summary>
        /// Gets or Sets Disease_id.
        /// </summary
        public long Disease_id { get; set; }

        /// <summary>
        /// Gets or Sets Disease_name.
        /// </summary
        public string Disease_name { get; set; }
    }

    /// <summary>
    /// CropStage.
    /// </summary>
    public class CropStage
    {
        /// <summary>
        /// Gets or Sets Crop_state.
        /// </summary
        public string Crop_state { get; set; }

        /// <summary>
        /// Gets or Sets Crop_state_id.
        /// </summary
        public int Crop_state_id { get; set; }
    }

    /// <summary>
    /// CropStageName.
    /// </summary>
    public class CropStageName
    {
        /// <summary>
        /// Gets or Sets Stage_Name.
        /// </summary
        public string Stage_Name { get; set; }

        /// <summary>
        /// Gets or Sets State_id.
        /// </summary
        public int State_id { get; set; }
    }

    /// <summary>
    /// ApprovedAreaCoverageRes.
    /// </summary>
    public class ApprovedAreaCoverageRes
    {
        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets AreaCovered_Value.
        /// </summary
        public decimal AreaCovered_Value { get; set; }
    }
}
