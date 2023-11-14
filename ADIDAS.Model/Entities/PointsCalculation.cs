// <copyright file="PointsCalculation.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// PointsCalculation.
    /// </summary>
    public class PointsCalculation
    {
        /// <summary>
        /// PointsCalculation.
        /// </summary>
        public PointsCalculation()
        {
            this.TotalActivityBasedCalculations = new List<TotalActivityBasedCalculation>();

            this.TotalActivityBasedCalculationsToday = new List<TotalActivityBasedCalculation>();

            this.TotalActivityBasedCalculationsYesterday = new List<TotalActivityBasedCalculation>();

            this.TotalActivityBasedCalculationsWeek = new List<TotalActivityBasedCalculation>();

            this.TotalActivityBasedCalculationsMonth = new List<TotalActivityBasedCalculation>();
        }

        /// <summary>
        /// Gets or Sets Total_Points.
        /// </summary
        public int? Total_Points { get; set; }

        /// <summary>
        /// Gets or Sets Today_Points.
        /// </summary
        public int? Today_Points { get; set; }

        /// <summary>
        /// Gets or Sets Yesterday_Points.
        /// </summary
        public int? Yesterday_Points { get; set; }

        /// <summary>
        /// Gets or Sets This_Week_Points.
        /// </summary
        public int? This_Week_Points { get; set; }

        /// <summary>
        /// Gets or Sets This_Month_Points.
        /// </summary
        public int? This_Month_Points { get; set; }

        /// <summary>
        /// Gets or Sets Previous_Year_Points.
        /// </summary
        public int? Previous_Year_Points { get; set; }

        /// <summary>
        /// Gets or Sets Previous_Year.
        /// </summary
        public string Previous_Year { get; set; }

        /// <summary>
        /// Gets or Sets TotalActivityBasedCalculations.
        /// </summary
        public List<TotalActivityBasedCalculation> TotalActivityBasedCalculations { get; set; }

        /// <summary>
        /// Gets or Sets TotalActivityBasedCalculationsToday.
        /// </summary
        public List<TotalActivityBasedCalculation> TotalActivityBasedCalculationsToday { get; set; }

        /// <summary>
        /// Gets or Sets TotalActivityBasedCalculationsYesterday.
        /// </summary
        public List<TotalActivityBasedCalculation> TotalActivityBasedCalculationsYesterday { get; set; }

        /// <summary>
        /// Gets or Sets TotalActivityBasedCalculationsWeek.
        /// </summary
        public List<TotalActivityBasedCalculation> TotalActivityBasedCalculationsWeek { get; set; }

        /// <summary>
        /// Gets or Sets TotalActivityBasedCalculationsMonth.
        /// </summary
        public List<TotalActivityBasedCalculation> TotalActivityBasedCalculationsMonth { get; set; }
    }
}
