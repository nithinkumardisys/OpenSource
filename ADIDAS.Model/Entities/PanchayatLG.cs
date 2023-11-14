//----------------------------------------------------------------------------
// <copyright file="PanchayatLG.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// PanchayatLG.
    /// </summary>
    public class PanchayatLG
    {
        /// <summary>
        /// Gets or Sets DistLgCode.
        /// </summary>
        public int DistLgCode { get; set; }

        /// <summary>
        /// Gets or Sets DistName.
        /// </summary>
        public string DistName { get; set; }

        /// <summary>
        /// Gets or Sets BlockLgCode.
        /// </summary>
        public int BlockLgCode { get; set; }

        /// <summary>
        /// Gets or Sets BlockName.
        /// </summary>
        public string BlockName { get; set; }

        /// <summary>
        /// Gets or Sets NaPanchayatLgCodeme.
        /// </summary>
        public int PanchayatLgCode { get; set; }

        /// <summary>
        /// Gets or Sets PanchayatName.
        /// </summary>
        public string PanchayatName { get; set; }

        /// <summary>
        /// Gets or Sets Villagelgcode.
        /// </summary>
        public long Villagelgcode { get; set; }

        /// <summary>
        /// Gets or Sets Villagename.
        /// </summary>
        public string Villagename { get; set; }
    }
}
