//------------------------------------------------------------------------------
// <copyright file="DistrictsBlocks.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// DistrictsBlocks.
    /// </summary>
    public class DistrictsBlocks
    {
        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int? District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Blocks.
        /// </summary>
        public List<BlocksList> Blocks { get; set; }
    }

    /// <summary>
    /// BlocksList.
    /// </summary>
    public class BlocksList
    {
        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int? Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int? District_id { get; set; }
    }
}
