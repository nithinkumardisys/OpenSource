//------------------------------------------------------------------------------
// <copyright file="ILgDirService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Service.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;

    /// <summary>
    /// ILgDirService.
    /// </summary>
    public interface ILgDirService
    {
        /// <summary>
        /// GetLGDirectoryDistrictData.
        /// </summary>
        /// <returns>District list.</returns>
        List<District> GetLGDirectoryDistrictData();

        /// <summary>
        /// GetLGDirectoryBlockData.
        /// </summary>
        /// <param name="districtlgCode">districtlgCode.</param>
        /// <returns>BlockResponse.</returns>
        BlockResponse GetLGDirectoryBlockData(string districtlgCode);

        /// <summary>
        /// GetLGDirectoryPanchayatData.
        /// </summary>
        /// <param name="districtlgCode">districtlgCode.</param>
        /// <param name="blockLgCode">blockLgCode.</param>
        /// <returns>PanchayatResponse.</returns>
        PanchayatResponse GetLGDirectoryPanchayatData(string districtlgCode, string blockLgCode);

        /*/// <summary>
        /// GetLGDirectoryVillageData.
        /// </summary>
        /// <param name="districtlgCode">districtlgCode.</param>
        /// <param name="blockLgCode">blockLgCode.</param>
        /// <param name="panchayatLgCode">panchayatLgCode.</param>
        /// <returns>VillageResponse.</returns>
        PanchayatResponse GetLGDirectoryVillageData(string districtlgCode, string blockLgCode, string panchayatLgCode);*/

        List<LgDirectoryPanchayatDim> GetLGDirectoryUserForm();
    }
}
