//------------------------------------------------------------------------------
// <copyright file="ViewFeedback.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// CreateViewFeedbackModel.
    /// </summary>
    public class ViewFeedback
    {
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
        /// Gets or Sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Designation.
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or Sets Date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or Sets Feedback.
        /// </summary>
        public string Feedback { get; set; }

        /// <summary>
        /// Gets or Sets SerialNo.
        /// </summary>
        public string SerialNo { get; set; }

        /// <summary>
        /// Gets or Sets DivisionName.
        /// </summary>
        public string DivisionName { get; set; }

        /// <summary>
        /// Gets or Sets SubDivisionName.
        /// </summary>
        public string SubDivisionName { get; set; }
    }
}
