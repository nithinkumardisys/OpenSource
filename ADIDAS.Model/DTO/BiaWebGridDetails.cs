//------------------------------------------------------------------------------
// <copyright file="BiaWebGridDetails.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// BiaWebGridDetails.
    /// </summary>
    public class BiaWebGridDetails
    {
        /// <summary>
        /// Gets or Sets BeneficaryId.
        /// </summary>
        public string BeneficiaryId { get; set; }

        /// <summary>
        /// Gets or Sets BeneficaryName.
        /// </summary>
        public string BeneficiaryName { get; set; }

        /// <summary>
        /// Gets or Sets AssignAoName.
        /// </summary>
        public string AssignAoName { get; set; }

        /// <summary>
        /// Gets or Sets RegistrationNo.
        /// </summary>
        public string RegistrationNo { get; set; }

        /// <summary>
        /// Gets or Sets Designation.
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or Sets DistrictName.
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or Sets SchemeName.
        /// </summary>
        public string SchemeName { get; set; }

        /// <summary>
        /// Gets or Sets PanchayatName.
        /// </summary>
        public string PanchayatName { get; set; }

        /// <summary>
        /// Gets or Sets BlockName.
        /// </summary>
        public string BlockName { get; set; }

        /// <summary>
        /// Gets or Sets BeneficaryType.
        /// </summary>
        public string BeneficiaryType { get; set; }

        /// <summary>
        /// Gets or Sets ApplicationNo.
        /// </summary>
        public string ApplicationNo { get; set; }

        /// <summary>
        /// Gets or Sets MobileNo.
        /// </summary>
        public string MobileNo { get; set; }

        /// <summary>
        /// Gets or Sets SubsidyReceived.
        /// </summary>
        public string SubsidyReceived { get; set; }

        /// <summary>
        /// Gets or Sets TransactionDate.
        /// </summary>
        public string TransactionDate { get; set; }

        /// <summary>
        /// Gets or Sets TargetDateOfCompletion.
        /// </summary>
        public DateTime TargetDateOfCompletion { get; set; }

        /// <summary>
        /// Gets or Sets Comments.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets AssignorOfficerUserId.
        /// </summary>
        public int? AssignorOfficerUserId { get; set; }

        /// <summary>
        /// Gets or Sets AssignAoID.
        /// </summary>
        public int? AssignAoID { get; set; }
    }
}
