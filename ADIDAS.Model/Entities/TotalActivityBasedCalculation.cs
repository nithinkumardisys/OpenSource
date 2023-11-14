// <copyright file="TotalActivityBasedCalculation.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// TotalActivityBasedCalculation.
    /// </summary>
    public class TotalActivityBasedCalculation
    {
        /// <summary>
        /// Gets or Sets Activity.
        /// </summary>
        public string Activity { get; set; }

        /// <summary>
        /// Gets or Sets Points_per_activity.
        /// </summary>
        public int? Points_per_activity { get; set; }

        /// <summary>
        /// Gets or Sets Activity_cnt.
        /// </summary>
        public int? Activity_cnt { get; set; }

        /// <summary>
        /// Gets or Sets Total_points.
        /// </summary>
        public int? Total_points { get; set; }
    }
}
