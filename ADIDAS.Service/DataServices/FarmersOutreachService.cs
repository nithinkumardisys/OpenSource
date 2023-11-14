//------------------------------------------------------------------------------
// <copyright file="FarmersOutreachService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Service.DataServices
{
    using System;
    using System.Collections.Generic;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.Interfaces;

    /// <summary>
    /// FarmersOutreachService.
    /// </summary>
    public class FarmersOutreachService : IFarmersOutreachService
    {
        private readonly IFarmersOutreachServiceRepository farmersOutreachServiceRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FarmersOutreachService"/> class.
        /// FarmersOutreachService.
        /// </summary>
        /// <param name="farmersOutreachServiceRepository">farmersOutreachServiceRepository.</param>
        public FarmersOutreachService(IFarmersOutreachServiceRepository farmersOutreachServiceRepository)
        {
            this.farmersOutreachServiceRepository = farmersOutreachServiceRepository;
        }

        /// <summary>
        /// GetGenderList.
        /// </summary>
        /// <returns>GenderList.</returns>
        public List<GenderList> GetGenderList()
        {
            return this.farmersOutreachServiceRepository.GetGenderList();
        }

        /// <summary>
        /// GetCategory.
        /// </summary>
        /// <returns>CategoryList.</returns>
        public List<Category> GetCategory()
        {
            return this.farmersOutreachServiceRepository.GetCategory();
        }

        /// <summary>
        /// GetFarmerCaste.
        /// </summary>
        /// <returns>FarmerCasteList.</returns>
        public List<FarmerCaste> GetFarmerCaste()
        {
            return this.farmersOutreachServiceRepository.GetFarmerCaste();
        }

        /// <summary>
        /// GetFarmerType.
        /// </summary>
        /// <returns>FarmerTypeList.</returns>
        public List<FarmerTypes> GetFarmerType()
        {
            return this.farmersOutreachServiceRepository.GetFarmerType();
        }

        /// <summary>
        /// GetTypeOfInteraction.
        /// </summary>
        /// <returns>TypeOfInteractionList.</returns>
        public List<TypeOfInteraction> GetTypeOfInteraction()
        {
            return this.farmersOutreachServiceRepository.GetTypeOfInteraction();
        }

        /// <summary>
        /// GetFosData.
        /// </summary>
        /// <param name="user_id">user_id.</param>
        /// <param name="date_range_start">date_range_start.</param>
        /// <param name="date_range_end">date_range_end.</param>
        /// <param name="isResolved">isResolved.</param>
        /// <returns>FosData.</returns>
        public FosData GetFosData(int user_id, DateTime date_range_start, DateTime date_range_end, string isResolved)
        {
            return this.farmersOutreachServiceRepository.GetFosData(user_id, date_range_start, date_range_end, isResolved);
        }

        /// <summary>
        /// PostSingleFarmerData.
        /// </summary>
        /// <param name="fosFarmerData">fosFarmerData.</param>
        /// <returns>output.</returns>
        public int PostSingleFarmerData(FosFarmerData fosFarmerData)
        {
            return this.farmersOutreachServiceRepository.PostSingleFarmerData(fosFarmerData);
        }

        /// <summary>
        /// PostGroupData.
        /// </summary>
        /// <param name="fosFarmerData">fosFarmerData.</param>
        /// <returns>output.</returns>
        public int PostGroupData(FosFarmerData fosFarmerData)
        {
            return this.farmersOutreachServiceRepository.PostGroupData(fosFarmerData);
        }
    }
}
