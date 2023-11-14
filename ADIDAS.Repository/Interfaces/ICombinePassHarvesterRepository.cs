//------------------------------------------------------------------------------
// <copyright file="ICombinePassHarvesterRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Repository.Interfaces
{
    using System.Collections.Generic;
    using ADIDAS.Model.DTO;

    /// <summary>
    /// ICombinePassHarvesterRepository.
    /// </summary>
    public interface ICombinePassHarvesterRepository
    {
        /// <summary>
        /// GetCombinePassHarvester.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="districtId">districtId.</param>
        /// <param name="machineTypeId">machineTypeId.</param>
        /// <returns>CombinePassHarvesterModelList.</returns>
        List<CombinePassHarvesterModel> GetCombinePassHarvester(string seasonId, int districtId, int machineTypeId);

        /// <summary>
        /// GetMachineType.
        /// </summary>
        /// <returns>CombinePassMachinery.</returns>
        List<CombinePassMachinery> GetMachineType();

        /// <summary>
        /// PostCombinePassHarvester.
        /// </summary>
        /// <param name="combinePassHarvesterModel">combinePassHarvesterModel.</param>
        /// <returns>int.</returns>
        int PostCombinePassHarvester(List<CombinePassHarvesterModel> combinePassHarvesterModel);
    }
}
