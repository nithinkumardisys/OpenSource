//------------------------------------------------------------------------------
// <copyright file="CombinePassHarvesterService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Service.DataServices
{
    using ADIDAS.Model.DTO;
    using ADIDAS.Repository.DataRepository;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.Interfaces;
    using System.Collections.Generic;

    /// <summary>
    /// CombinePassHarvesterService.
    /// </summary>
    public class CombinePassHarvesterService : ICombinePassHarvesterService
    {
        /// <summary>
        /// ICombinePassHarvesterRepository.
        /// </summary>
        private ICombinePassHarvesterRepository _combinePassHarvesterRepository;

        /// <summary>
        /// CombinePassHarvesterService.
        /// </summary>
        /// <param name="combinePassHarvesterRepository">combinePassHarvesterRepository.</param>
        public CombinePassHarvesterService(ICombinePassHarvesterRepository combinePassHarvesterRepository)
        {
            _combinePassHarvesterRepository = combinePassHarvesterRepository;
        }

        /// <summary>
        /// GetCombinePassHarvester.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="districtId">districtId.</param>
        /// <param name="machineTypeId">machineTypeId.</param>
        /// <returns>CombinePassHarvesterModelList.</returns>
        public List<CombinePassHarvesterModel> GetCombinePassHarvester(string seasonId, int districtId, int machineTypeId)
        {
            return _combinePassHarvesterRepository.GetCombinePassHarvester(seasonId, districtId, machineTypeId);
        }

        /// <summary>
        /// GetMachineType.
        /// </summary>
        /// <returns>CombinePassMachinery.</returns>
        public List<CombinePassMachinery> GetMachineType()
        {
            return _combinePassHarvesterRepository.GetMachineType();
        }

        /// <summary>
        /// PostCombinePassHarvester.
        /// </summary>
        /// <param name="combinePassHarvesterModel">combinePassHarvesterModel.</param>
        /// <returns>int.</returns>
        public int PostCombinePassHarvester(List<CombinePassHarvesterModel> combinePassHarvesterModel)
        {
            return _combinePassHarvesterRepository.PostCombinePassHarvester(combinePassHarvesterModel);
        }
    }
}
