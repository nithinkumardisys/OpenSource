// <copyright file="SoilHealth.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// SoilHealth.
    /// </summary>
    public class SoilHealth
    {
        /// <summary>
        /// Gets or Sets DistCode.
        /// </summary>
        public string DistCode { get; set; }

        /// <summary>
        /// Gets or Sets DistrictName.
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or Sets Target.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Gets or Sets Collected.
        /// </summary>
        public string Collected { get; set; }

        /// <summary>
        /// Gets or Sets ReceivedInLab.
        /// </summary>
        public string ReceivedInLab { get; set; }

        /// <summary>
        /// Gets or Sets Reported.
        /// </summary>
        public string Reported { get; set; }

        /// <summary>
        /// Gets or Sets Distributed.
        /// </summary>
        public string Distributed { get; set; }
    }
}
