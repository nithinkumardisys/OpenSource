//------------------------------------------------------------------------------
// <copyright file="PiasDataReportInputModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// GetPiasDataReportInputModel.
    /// </summary>
    public class GetPiasDataReportInputModel
    {
        /// <summary>
        /// Gets or Sets Report_Type.
        /// </summary>
        public string Report_Type { get; set; }

        /// <summary>
        /// Gets or Sets District_ID.
        /// </summary>
        public string District_ID { get; set; }

        /// <summary>
        /// Gets or Sets Market_Id.
        /// </summary>
        public string Market_Id { get; set; }

        /// <summary>
        /// Gets or Sets Comodity_Group_Id.
        /// </summary>
        public string Comodity_Group_Id { get; set; }

        /// <summary>
        /// Gets or Sets Comodity_Id.
        /// </summary>
        public string Comodity_Id { get; set; }

        /// <summary>
        /// Gets or Sets Variety_Id.
        /// </summary>
        public string Variety_Id { get; set; }

        /// <summary>
        /// Gets or Sets Date.
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Gets or Sets From_Date.
        /// </summary>
        public DateTime? From_Date { get; set; }

        /// <summary>
        /// Gets or Sets To_Date.
        /// </summary>
        public DateTime? To_Date { get; set; }
    }
}
