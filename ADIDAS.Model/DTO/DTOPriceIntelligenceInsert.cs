//------------------------------------------------------------------------------
// <copyright file="DTOPriceIntelligenceInsert.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Price Intelligence Insert.
    /// </summary>
    public class DtoPriceIntelligenceInsert
    {
        /// <summary>
        /// Gets or Sets PanchayatId.
        /// </summary>
        public int PanchayatId { get; set; }

        /// <summary>
        /// Gets or Sets ReportedDate.
        /// </summary>
        public DateTime ReportedDate { get; set; }

        /// <summary>
        /// Gets or Sets NodeId.
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// Gets or Sets NodeName.
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// Gets or Sets CropId.
        /// </summary>
        public int CropId { get; set; }

        /// <summary>
        /// Gets or Sets CropName.
        /// </summary>
        public string CropName { get; set; }

        /// <summary>
        /// Gets or Sets RateInQuintal.
        /// </summary>
        public decimal RateInQuintal { get; set; }

        /// <summary>
        /// Gets or Sets ReccreatedUserId.
        /// </summary>
        public int ReccreatedUserId { get; set; }

        /// <summary>
        /// Gets or Sets RecCreatedDate.
        /// </summary>
        public DateTime RecCreatedDate { get; set; }
    }
}
