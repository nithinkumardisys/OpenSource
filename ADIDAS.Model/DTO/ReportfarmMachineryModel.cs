//------------------------------------------------------------------------------
// <copyright file="ReportfarmMachineryModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Report farm Machinery Model.
    /// </summary>
    public class ReportfarmMachineryModel
    {
        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Machinery_ID.
        /// </summary>
        public int Machinery_ID { get; set; }

        /// <summary>
        /// Gets or Sets Machinery_qty.
        /// </summary>
        public int Machinery_qty { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Response_status.
        /// </summary>
        public string Response_status { get; set; }
    }
}
