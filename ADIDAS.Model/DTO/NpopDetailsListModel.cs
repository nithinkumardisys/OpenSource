//------------------------------------------------------------------------------
// <copyright file="NpopDetailsListModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// NPOP Details.
    /// </summary>
    public class NpopDetailsListModel
    {
        /// <summary>
        /// Gets or Sets FarmerName.
        /// </summary>
        public string FarmerName { get; set; }

        /// <summary>
        /// Gets or Sets FarmerDbtRegNo.
        /// </summary>
        public string FarmerDbtRegNo { get; set; }

        /// <summary>
        /// Gets or Sets PhoneNum.
        /// </summary>
        public string PhoneNum { get; set; }

        /// <summary>
        /// Gets or Sets BlockName.
        /// </summary>
        public string BlockName { get; set; }

        /// <summary>
        /// Gets or Sets PanchayatName.
        /// </summary>
        public string PanchayatName { get; set; }

        /// <summary>
        /// Gets or Sets NoOfFarms.
        /// </summary>
        public int NoOfFarms { get; set; }

        /// <summary>
        /// Gets or Sets NoOfCrops.
        /// </summary>
        public int NoOfCrops { get; set; }

        /// <summary>
        /// Gets or Sets StatusF.
        /// </summary>
        public string StatusF { get; set; }
    }
}
