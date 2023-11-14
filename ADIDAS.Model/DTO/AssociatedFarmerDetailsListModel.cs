//------------------------------------------------------------------------------
// <copyright file="AssociatedFarmerDetailsListModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// AssociatedFarmer Details.
    /// </summary>
    public class AssociatedFarmerDetailsListModel
    {
        /// <summary>
        /// Gets or Sets FarmerName.
        /// </summary>
        public string FarmerName { get; set; }

        /// <summary>
        /// Gets or Sets GroupName.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or Sets FarmerDbtRegNo.
        /// </summary>
        public string FarmerDbtRegNo { get; set; }

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
        /// Gets or Sets StatusF.
        /// </summary>
        public string StatusF { get; set; }
    }

    /// <summary>
    /// PGSGroupNames Details.
    /// </summary>
    public class PGSGroupNames
    {
        /// <summary>
        /// Gets or Sets GroupName.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or Sets GroupId.
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// Gets or Sets GroupRegNo.
        /// </summary>
        public string GroupRegNo { get; set; }
    }

    // <summary>
    /// PGSGroupNamesAndDBTRegNo Details.
    /// </summary>
    public class PGSGroupNamesAndDBTRegNo
    {
        /// <summary>
        /// Gets or Sets GroupName.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or Sets GroupRegNo.
        /// </summary>
        public long GroupRegNo { get; set; }
    }

    // <summary>
    /// PGSGroupNamesAndDbtNo Details.
    /// </summary>
    public class PGSGroupNamesAndDbtNo
    {
        /// <summary>
        /// Gets or Sets Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets GroupName.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or Sets GroupDbtNo.
        /// </summary>
        public string GroupDbtNo { get; set; }
    }
}
