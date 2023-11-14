//------------------------------------------------------------------------------
// <copyright file="SoilConservationService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Service.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.DataRepository;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.Interfaces;

    /// <summary>
    /// Soil Conservation Service
    /// </summary>
    public class SoilConservationService : ISoilConservationService
    {
        /// <summary>
        /// Interface Soil Conservation Repository.
        /// </summary>
        private ISoilConservationRepository _soilConservationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoilConservationService"/> class.
        /// Soil Conservation Service.
        /// </summary>
        /// <param name="soilConservationRepository">soilConservationRepository.</param>
        public SoilConservationService(ISoilConservationRepository soilConservationRepository)
        {
            _soilConservationRepository = soilConservationRepository;
        }

        /// <summary>
        /// Get Yojna Number List
        /// </summary>
        /// <returns> YojnaNumberList</returns>
        public List<SoilConservationYojnaNumberResponse> GetYojnaNumberList()
        {
            return this._soilConservationRepository.GetYojnaNumberList();
        }

        /// <summary>
        /// Get Yojna Name List
        /// </summary>
        /// <returns> YojnaNameList</returns>
        public List<SoilConservationYojnaNameResponse> GetYojnaNameList()
        {
            return this._soilConservationRepository.GetYojnaNameList();
        }

        /// <summary>
        /// Get Dbt Mobile Number
        /// </summary>
        /// <returns> Dbt Mobile Number Details</returns>

        /// <summary>
        /// Get Structure Type Soil Conservation
        /// </summary>
        /// <returns> Structure Type Soil Conservation Info</returns>
        public SoilConservationStructureType GetStructureTypeSoilConservation()
        {
            return this._soilConservationRepository.GetStructureTypeSoilConservation();
        }

        /// <summary>
        /// Get Plant Soil Conservation
        /// </summary>
        /// <returns> Plant Soil Conservation Info </returns>
        public List<SoilConservationPlantResponse> GetPlantSoilConservation()
        {
            return this._soilConservationRepository.GetPlantSoilConservation();
        }

        /// <summary>
        /// Get Crop Soil Conservation.
        /// </summary>
        /// <returns> Crop Soil Conservation Info. </returns>
        public List<SoilConservationCropResponse> GetCropSoilConservation()
        {
            return this._soilConservationRepository.GetCropSoilConservation();
        }

        /// <summary>
        /// Get Input Type Soil Conservation.
        /// </summary>
        /// <returns> InputType Soil Conservation Info. </returns>
        public List<SoilConservationInputTypeResponse> GetInputTypeSoilConservation()
        {
            return this._soilConservationRepository.GetInputTypeSoilConservation();
        }

        /// <summary>
        /// Get Topic Name Soil Conservation
        /// </summary>
        /// <returns> Topic Name Soil Conservation Info </returns>
        public List<SoilConservationTopicName> GetTopicNameSoilConservation()
        {
            return this._soilConservationRepository.GetTopicNameSoilConservation();
        }

        /// <summary>
        /// Get Activity SubActivity Details
        /// </summary>
        /// <returns> Activity SubActivity List</returns>
        public List<SoilConservationActivityDetails> GetActivitySubActivityDetails()
        {
            return this._soilConservationRepository.GetActivitySubActivityDetails();
        }

        /// <summary>
        /// Get Soil Conservation Submitted Data
        /// </summary>
        /// <returns> Soil Conservation Submitted Data Details</returns>
        public List<SoilConservationSubmittedDataResponse> GetSoilConservationSubmittedData(int panchayat_id, int scheme_id)
        {
            return this._soilConservationRepository.GetSoilConservationSubmittedData(panchayat_id, scheme_id);
        }

        /// <summary>
        /// Soil Conservation Submitted Data
        /// </summary>
        /// <returns>Soil Conservation Submitted Data Details</returns>
        public List<SoilConservationNotificationResponse> GetSoilConservationNotification(string panchayat_id)
        {
            return this._soilConservationRepository.GetSoilConservationNotification(panchayat_id);
        }

        /// <summary>
        /// Soil Conservation Existing Physical Financial Target
        /// </summary>
        /// <returns>Soil Conservation Submitted Data Details</returns>
        public List<SoilConservationExistingPhysicalFinancialTarget> GetExistingPhysicalFinancialTarget(int panchayat_id)
        {
            return this._soilConservationRepository.GetExistingPhysicalFinancialTarget(panchayat_id);
        }

        /// <summary>
        /// Get Post Soil Conservation
        /// </summary>
        /// <returns> Post Soil Conservation Result </returns>
        public bool PostSoilConservation(List<SoilConservationCreateRequest> PostSoilConservation)
        {
            return this._soilConservationRepository.PostSoilConservation(PostSoilConservation);
        }
    }
}
