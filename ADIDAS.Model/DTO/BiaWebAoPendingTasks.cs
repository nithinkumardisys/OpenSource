//------------------------------------------------------------------------------
// <copyright file="BiaWebAoPendingTasks.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// BiaWebAoPendingTasks.
    /// </summary>
    public class BiaWebAoPendingTasks
    {
        /// <summary>
        /// Gets or Sets Scheme.
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Gets or Sets BeneficaryName.
        /// </summary>
        public string BeneficiaryName { get; set; }

        /// <summary>
        /// Gets or Sets BeneficaryId.
        /// </summary>
        public string BeneficiaryId { get; set; }

        /// <summary>
        /// Gets or Sets RegistrationNo.
        /// </summary>
        public string RegistrationNo { get; set; }

        /// <summary>
        /// Gets or Sets DistrictName.
        /// </summary>
        public string DistrictName { get; set; }
    }
}