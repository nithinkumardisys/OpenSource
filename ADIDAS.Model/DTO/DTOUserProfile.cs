//------------------------------------------------------------------------------
// <copyright file="DTOUserProfile.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// UserProfile.
    /// </summary>
    public class DtoUserProfile
    {
        /// <summary>
        /// Gets or Sets UserId.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets FirstName.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or Sets UserPassword.
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// Gets or Sets LastName.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or Sets PhoneNumber.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or Sets PreferedLanguage.
        /// </summary>
        public string PreferedLanguage { get; set; }

        /// <summary>
        /// Gets or Sets Gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or Sets ImageData.
        /// </summary>
        public string ImageData { get; set; }

        /// <summary>
        /// Gets or Sets ImageFileName.
        /// </summary>
        public string ImageFileName { get; set; }

        /// <summary>
        /// Gets or Sets ImageFileLocation.
        /// </summary>
        public string ImageFileLocation { get; set; }
    }
}
