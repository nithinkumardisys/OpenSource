//------------------------------------------------------------------------------
// <copyright file="ErrorModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Error Model.
    /// </summary>
    public class ErrorModel
    {
        /// <summary>
        /// Gets or Sets UserId.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or Sets ActivityDesription.
        /// </summary>
        public string ActivityDesription { get; set; }

        /// <summary>
        /// Gets or Sets ActivityStatus.
        /// </summary>
        public string ActivityStatus { get; set; }

        /// <summary>
        /// Gets or Sets ActivityTF.
        /// </summary>
        public DateTime ActivityTF { get; set; }

        /// <summary>
        /// Gets or Sets ActivitySource.
        /// </summary>
        public string ActivitySource { get; set; }

        /// <summary>
        /// Gets or Sets ApiSource.
        /// </summary>
        public string ApiSource { get; set; }

        /// <summary>
        /// Gets or Sets ActivityType.
        /// </summary>
        public string ActivityType { get; set; }

        /// <summary>
        /// Gets or Sets RetValue.
        /// </summary>
        public int RetValue { get; set; }
    }
}
