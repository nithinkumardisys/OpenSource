// <copyright file="MobileAppConfig.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// MobileAppConfig.
    /// </summary>
    public partial class MobileAppConfig
    {
        /// <summary>
        /// Gets or Sets TileName.
        /// </summary>
        public string TileName { get; set; }

        /// <summary>
        /// Gets or Sets LabelName.
        /// </summary>
        public string LabelName { get; set; }

        /// <summary>
        /// Gets or Sets InputType.
        /// </summary>
        public string InputType { get; set; }

        /// <summary>
        /// Gets or Sets InputDatatype.
        /// </summary>
        public string InputDatatype { get; set; }

        /// <summary>
        /// Gets or Sets AccessRole.
        /// </summary>
        public string AccessRole { get; set; }

        /// <summary>
        /// Gets or Sets RecCreatedUserid.
        /// </summary>
        public string RecCreatedUserid { get; set; }

        /// <summary>
        /// Gets or Sets RecCreatedDate.
        /// </summary>
        public DateTime? RecCreatedDate { get; set; }

        /// <summary>
        /// Gets or Sets RecUpdatedUserid.
        /// </summary>
        public string RecUpdatedUserid { get; set; }

        /// <summary>
        /// Gets or Sets RecUpdatedDate.
        /// </summary>
        public DateTime? RecUpdatedDate { get; set; }
    }
}
