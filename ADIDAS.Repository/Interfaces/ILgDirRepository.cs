//------------------------------------------------------------------------------
// <copyright file="ILgDirRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Repository.Interfaces
{
    using System.Collections.Generic;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;

    /// <summary>
    /// ILgDirRepository.
    /// </summary>
    public interface ILgDirRepository
    {
        /// <summary>
        /// GetLGDirectoryDistrictData.
        /// </summary>
        /// <returns>District.</returns>
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

        List<LgDirectoryPanchayatDim> GetLGDirectoryUserForm();
    }
}
