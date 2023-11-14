//------------------------------------------------------------------------------
// <copyright file="InsBAOCropdmgModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Ins Bao Crop damage Model.
    /// </summary>
    public class InsBaoCropdmgModel
    {
        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public InsBaoCropdmgModel()
        {
            this.BlockList = new List<BlockList>();
        }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string Damage_Reason { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string Damage_Reason_id { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string District_Id { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string District_Name { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string Refreshed_userid { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string Dm_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public List<BlockList> BlockList { get; set; }
    }

    /// <summary>
    /// Block List.
    /// </summary>
    public class BlockList
    {
        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string Block_Id { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string Block_Name { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string Net_area_sown { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public int? DamageReasonId { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string Damage_Reason { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string Refreshed_userid { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string Refreshed_username { get; set; }

        /// <summary>
        /// Gets or Sets PanchayatList.
        /// </summary>
        public List<CropDamageDetailsGet> PanchayatList { get; set; }
    }
}
