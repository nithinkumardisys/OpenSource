//------------------------------------------------------------------------------
// <copyright file="SoilConservationStructureType.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// create Soil Conservation Structure Type.
    /// </summary>
    public class SoilConservationStructureType
    {
        /// <summary>
        /// Gets or Sets CommonList.
        /// </summary>
        public List<CommonList> CommonList { get; set; }

        /// <summary>
        /// Gets or Sets EntryPointList.
        /// </summary>
        public List<EntryPointList> EntryPointList { get; set; }
    }

    /// <summary>
    /// CommonList.
    /// </summary>
    public class CommonList
    {
        /// <summary>
        /// Gets or Sets Structure_Id.
        /// </summary>
        public int? Structure_Id { get; set; }

        /// <summary>
        /// Gets or Sets Structure_Name.
        /// </summary>
        public string Structure_Name { get; set; }
    }

    /// <summary>
    /// EntryPointList
    /// </summary>///
    public class EntryPointList
    {
        /// <summary>
        /// Gets or Sets Structure_Id.
        /// </summary>
        public int? Structure_Id { get; set; }

        /// <summary>
        /// Gets or Sets Structure_Name.
        /// </summary>
        public string Structure_Name { get; set; }
    }
}