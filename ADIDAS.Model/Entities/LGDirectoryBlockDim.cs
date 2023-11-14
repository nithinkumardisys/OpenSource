//------------------------------------------------------------------------------
// <copyright file="LGDirectoryBlockDim.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// LgDirectoryBlockDim.
    /// </summary>
    public partial class LgDirectoryBlockDim
    {
        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int? District_Id { get; set; }

        /// <summary>
        /// Gets or Sets District_Name.
        /// </summary>
        public string District_Name { get; set; }

        /// <summary>
        /// Gets or Sets Block_Id.
        /// </summary>
        public int? Block_Id { get; set; }

        /// <summary>
        /// Gets or Sets Block_Name.
        /// </summary>
        public string Block_Name { get; set; }
    }
}
