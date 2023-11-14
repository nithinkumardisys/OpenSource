//------------------------------------------------------------------------------
// <copyright file="GroupDetails.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// GroupDetails.
    /// </summary>
    public class GroupDetails
    {
        /// <summary>
        /// GroupDetails.
        /// </summary>
        public GroupDetails()
        {
            this.MyGroup = new List<SenderGroupDetails>();

            this.ReceiverGroup = new List<RecipientGroupDetails>();
        }

        /// <summary>
        /// Gets or Sets MyGroup.
        /// </summary>
        public List<SenderGroupDetails> MyGroup { get; set; }

        /// <summary>
        /// Gets or Sets ReceiverGroup.
        /// </summary>
        public List<RecipientGroupDetails> ReceiverGroup { get; set; }
    }
}
