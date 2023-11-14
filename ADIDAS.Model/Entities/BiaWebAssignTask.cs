//------------------------------------------------------------------------------
// <copyright file="BiaWebAssignTask.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
using System;

namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// BiaWebAssignTask.
    /// </summary>
    public class BiaWebAssignTask
    {
        /// <summary>
        /// Gets or Sets BeneficiaryId.
        /// </summary>
        public string BeneficiaryId { get; set; }

        /// <summary>
        /// Gets or Sets SelectedOfficer.
        /// </summary>
        public int SelectedOfficerUserId { get; set; }

        /// <summary>
        /// Gets or Sets AssigneeOfficerUserId.
        /// </summary>
        public int AssigneeOfficerUserId { get; set; }

        /// <summary>
        /// Gets or Sets TargetDate.
        /// </summary>
        public DateTime TargetDate { get; set; }
    }
}
