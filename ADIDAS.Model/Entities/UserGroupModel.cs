// <copyright file="UserGroupModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// UserGroupModel.
    /// </summary>
    public class UserGroupModel
    {
        /// <summary>
        /// Gets or Sets RecipientGroupModel.
        /// </summary>
        public RecipientGroupModel RecipientGroupModel { get; set; }

        /// <summary>
        /// Gets or Sets SenderGroupModel.
        /// </summary>
        public SenderGroupModel SenderGroupModel { get; set; }

        /// <summary>
        /// Gets or Sets CreatedBy.
        /// </summary>
        public string CreatedBy { get; set; }
    }
}
