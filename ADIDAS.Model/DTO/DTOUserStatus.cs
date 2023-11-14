//------------------------------------------------------------------------------
// <copyright file="DTOUserStatus.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// UserStatus.
    /// </summary>
    public class DtoUserStatus
    {
        /// <summary>
        /// Gets or Sets UserID.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets PrevStatus.
        /// </summary>
        public string PrevStatus { get; set; }
    }

    /// <summary>
    /// User Status Request.
    /// </summary>
    public class DtoUserStatusRequest
    {
        /// <summary>
        /// Gets or Sets UserList.
        /// </summary>
        public List<DtoUserStatus> UserList { get; set; }

        /// <summary>
        /// Gets or Sets Comments.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or Sets DaoId.
        /// </summary>
        public string DaoId { get; set; }
    }
}
