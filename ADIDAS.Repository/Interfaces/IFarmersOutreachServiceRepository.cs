//------------------------------------------------------------------------------
// <copyright file="IFarmersOutreachServiceRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using ADIDAS.Model.Entities;

    /// <summary>
    /// Interface IFarmersOutreachService Repo.
    /// </summary>
    public interface IFarmersOutreachServiceRepository
    {
        /// <summary>
        /// GetGenderList.
        /// </summary>
        /// <returns>Response.</returns>
        List<GenderList> GetGenderList();

        /// <summary>
        /// GetCategory.
        /// </summary>
        /// <returns>Response.</returns>
        List<Category> GetCategory();

        /// <summary>
        /// GetFarmerCaste.
        /// </summary>
        /// <returns>Response.</returns>
        List<FarmerCaste> GetFarmerCaste();

        /// <summary>
        /// GetFarmerType.
        /// </summary>
        /// <returns>Response.</returns>
        List<FarmerTypes> GetFarmerType();

        /// <summary>
        /// GetTypeOfInteraction.
        /// </summary>
        /// <returns>Response.</returns>
        List<TypeOfInteraction> GetTypeOfInteraction();

        /// <summary>
        /// GetFosData.
        /// </summary>
        /// <param name="user_id">user_id.</param>
        /// <param name="date_range_start">date_range_start.</param>
        /// <param name="date_range_end">date_range_end.</param>
        /// <param name="isResolved">isResolved.</param>
        /// <returns>FosData.</returns>
        FosData GetFosData(int user_id, DateTime date_range_start, DateTime date_range_end, string isResolved);

        /// <summary>
        /// PostSingleFarmerData.
        /// </summary>
        /// <param name="fosFarmerData">fosFarmerData.</param>
        /// <returns>output.</returns>
        int PostSingleFarmerData(FosFarmerData fosFarmerData);

        /// <summary>
        /// PostGroupData.
        /// </summary>
        /// <param name="fosFarmerData">fosFarmerData.</param>
        /// <returns>output.</returns>
        int PostGroupData(FosFarmerData fosFarmerData);
    }
}
