//------------------------------------------------------------------------------
// <copyright file="BlockLst.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// BlockLst.
    /// </summary>
    public class BlockLst
    {
        /// <summary>
        /// Gets or Sets BlockId.
        /// </summary>
        public int? BlockId { get; set; }

        /// <summary>
        /// Gets or Sets BlockName.
        /// </summary>
        public string BlockName { get; set; }
    }

    public class SubDivision
    {
        /// <summary>
        /// Gets or Sets SubDivisionId.
        /// </summary>
        public int? SubDivisionId { get; set; }

        /// <summary>
        /// Gets or Sets SubDivisionName.
        /// </summary>
        public string SubDivisionName { get; set; }
    }
}
