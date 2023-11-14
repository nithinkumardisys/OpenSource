//------------------------------------------------------------------------------
// <copyright file="AppReport.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// App Report.
    /// </summary>
    public partial class AppReport
    {
        /// <summary>
        /// Gets or Sets Report_Id.
        /// </summary>
        public int Report_Id { get; set; }

        /// <summary>
        /// Gets or Sets Report_Order_No.
        /// </summary>
        public int Report_Order_No { get; set; }

        /// <summary>
        /// Gets or Sets Report_Name.
        /// </summary>
        public string Report_Name { get; set; }

        /// <summary>
        /// Gets or Sets Report_Desc.
        /// </summary>
        public string Report_Desc { get; set; }

        /// <summary>
        /// Gets or Sets Report_Url.
        /// </summary>
        public string Report_Url { get; set; }

        /// <summary>
        /// Gets or Sets Report_Level.
        /// </summary>
        public int Report_Level { get; set; }

        /// <summary>
        /// Gets or Sets Parent_Report_Name.
        /// </summary>
        public string Parent_Report_Name { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Created_Userid.
        /// </summary>
        public string Rec_Created_Userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Created_Date.
        /// </summary>
        public DateTime? Rec_Created_Date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Updated_Userid.
        /// </summary>
        public string Rec_Updated_Userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Updated_Date.
        /// </summary>
        public DateTime? Rec_Updated_Date { get; set; }
    }
}
