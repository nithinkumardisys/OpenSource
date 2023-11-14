//------------------------------------------------------------------------------
// <copyright file="FosData.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// FosData.
    /// </summary>
    public class FosData
    {
        /// <summary>
        /// Gets or Sets SingleFarmer.
        /// </summary>
        public List<FosFarmerData> SingleFarmer { get; set; }

        /// <summary>
        /// Gets or Sets GroupOfFarmer.
        /// </summary>
        public List<FosFarmerData> GroupOfFarmer { get; set; }
    }
}
