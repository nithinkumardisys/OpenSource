//------------------------------------------------------------------------------
// <copyright file="FPODetails.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// FPO Details Model.
    /// </summary>
    public class FPODetails
    {
        /// <summary>
        /// Gets or Sets FpoId.
        /// </summary>
        public int FpoId { get; set; }

        /// <summary>
        /// Gets or Sets FpoName.
        /// </summary>
        public string FpoName { get; set; }

        /// <summary>
        /// Gets or Sets FpoAddress.
        /// </summary>
        public string FpoAddress { get; set; }

        /// <summary>
        /// Gets or Sets FpoContactPerson.
        /// </summary>
        public string FpoContactPerson { get; set; }

        /// <summary>
        /// Gets or Sets FpoPhoneNum.
        /// </summary>
        public string FpoPhoneNum { get; set; }
    }
}
