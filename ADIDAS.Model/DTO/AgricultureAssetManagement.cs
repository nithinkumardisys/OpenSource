//------------------------------------------------------------------------------
// <copyright file="AgricultureAssetManagement.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// To define class for generating agriculture asset reports.
    /// </summary>
    public class AgricultureAssetManagement
    {
        /// <summary>
        /// Gets or Sets UserId.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or Sets SelectedActivity.
        /// </summary>
        public string SelectedActivity { get; set; }

        /// <summary>
        /// Gets or Sets SelectedMachines.
        /// </summary>
        public string SelectedMachines { get; set; }

        /// <summary>
        /// Gets or Sets SelectedStructures.
        /// </summary>
        public string SelectedStructures { get; set; }

        /// <summary>
        /// Gets or Sets SelectedDistrict.
        /// </summary>
        public string SelectedDistrict { get; set; }

        /// <summary>
        /// Gets or Sets SelectedBlocks.
        /// </summary>
        public string SelectedBlocks { get; set; }

        /// <summary>
        /// Gets or Sets SelectedPanchayats.
        /// </summary>
        public string SelectedPanchayats { get; set; }

        /// <summary>
        /// Gets or Sets SelectedStatus.
        /// </summary>
        public string SelectedStatus { get; set; }

        /// <summary>
        /// Gets or Sets SelectedTemplate.
        /// </summary>
        public string SelectedTemplate { get; set; }
    }
}
