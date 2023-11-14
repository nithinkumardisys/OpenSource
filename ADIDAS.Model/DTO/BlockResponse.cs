//------------------------------------------------------------------------------
// <copyright file="BlockResponse.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Block Response.
    /// </summary>
    public class BlockResponse
    {
        /// <summary>
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or Sets District.
        /// </summary>
        public DtoLgDistrict District { get; set; }

        /// <summary>
        /// Gets or Sets Blocks.
        /// </summary>
        public List<DtoLgBlock> Blocks { get; set; }
    }
}
