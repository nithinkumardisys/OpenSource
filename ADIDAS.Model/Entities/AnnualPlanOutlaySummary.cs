//------------------------------------------------------------------------------
// <copyright file="AnnualPlanOutlaySummary.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// Annual Plan Outlay Summary.
    /// </summary>
    public class AnnualPlanOutlaySummary
    {
        /// <summary>
        /// Gets or Sets Scheme_id.
        /// </summary>
        public int Scheme_id { get; set; }

        /// <summary>
        /// Gets or Sets Budget_head_id.
        /// </summary>
        public int Budget_head_id { get; set; }

        /// <summary>
        /// Gets or Sets Total_allotment.
        /// </summary>
        public decimal Total_allotment { get; set; }

        /// <summary>
        /// Gets or Sets Total_expenses.
        /// </summary>
        public decimal Total_expenses { get; set; }

        /// <summary>
        /// Gets or Sets Total_balance.
        /// </summary>
        public decimal Total_balance { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_type.
        /// </summary>
        public string Scheme_type { get; set; }

        /// <summary>
        /// Gets or Sets Expense_dt.
        /// </summary>
        public DateTime Expense_dt { get; set; }

        /// <summary>
        /// Gets or Sets Expense.
        /// </summary>
        public decimal Expense { get; set; }

        /// <summary>
        /// Gets or Sets Expense_id.
        /// </summary>
        public int Expense_id { get; set; }

        /// <summary>
        /// Gets or Sets Expense_created_userid.
        /// </summary>
        public int Expense_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Expense_created_date.
        /// </summary>
        public DateTime Expense_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Budget_head_name.
        /// </summary>
        public string Budget_head_name { get; set; }

        /// <summary>
        /// Gets or Sets Sub_code.
        /// </summary>
        public string Sub_code { get; set; }
    }
}
