//------------------------------------------------------------------------------
// <copyright file="AnnualPlanOutlayModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// Annual Plan Outlay Model.
    /// </summary>
    public class AnnualPlanOutlayModel
    {
        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_ID.
        /// </summary>
        public int Scheme_ID { get; set; }

        /// <summary>
        /// Gets or Sets Flag.
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// Gets or Sets Budget_head_id.
        /// </summary>
        public int Budget_head_id { get; set; }

        /// <summary>
        /// Gets or Sets Total_allotment.
        /// </summary>
        public decimal? Total_allotment { get; set; }

        /// <summary>
        /// Gets or Sets Total_expense.
        /// </summary>
        public decimal? Total_expense { get; set; }

        /// <summary>
        /// Gets or Sets Total_balance.
        /// </summary>
        public decimal? Total_balance { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }
    }
}
