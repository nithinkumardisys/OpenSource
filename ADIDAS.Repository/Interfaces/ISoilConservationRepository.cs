//------------------------------------------------------------------------------
// <copyright file="ISoilConservationRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Repository.Interfaces
{
    using System.Collections.Generic;
    using ADIDAS.Model.DTO;

    public interface ISoilConservationRepository
    {
        /// <summary>
        /// Get Yojna Number List.
        /// </summary>
        /// <returns> YojnaNumberList.</returns>
        List<SoilConservationYojnaNumberResponse> GetYojnaNumberList();

        /// <summary>
        /// Get Yojna Name List.
        /// </summary>
        /// <returns> YojnaNameList.</returns>
        List<SoilConservationYojnaNameResponse> GetYojnaNameList();

        /// <summary>
        /// GetDbtMobileNumber
        /// </summary>
        /// <returns>DbtMobileNumber</returns>
        List<SoilConservationDtbNumberResponse> GetDbtMobileNumber(int registrationNumber);

        /// <summary>
        /// Get Structure Type Soil Conservation.
        /// </summary>
        /// <returns> Structure Type Soil Conservation Info.</returns>
        SoilConservationStructureType GetStructureTypeSoilConservation();

        /// <summary>
        /// Get Plant Soil Conservation.
        /// </summary>
        /// <returns> Plant Soil Conservation Info </returns>
        List<SoilConservationPlantResponse> GetPlantSoilConservation();

        /// <summary>
        /// Get Crop Soil Conservation.
        /// </summary>
        /// <returns> Crop Soil Conservation Info. </returns>
        List<SoilConservationCropResponse> GetCropSoilConservation();

        /// <summary>
        /// Get Input Type Soil Conservation.
        /// </summary>
        /// <returns> InputType Soil Conservation Info </returns>
        List<SoilConservationInputTypeResponse> GetInputTypeSoilConservation();

        /// <summary>
        /// Get Topic Name Soil Conservation.
        /// </summary>
        /// <returns> Topic Name Soil Conservation Info. </returns>
        List<SoilConservationTopicName> GetTopicNameSoilConservation();

        /// <summary>
        /// Get Activity SubActivity Details.
        /// </summary>
        /// <returns> Activity SubActivity List.</returns>
        List<SoilConservationActivityDetails> GetActivitySubActivityDetails();

        /// <summary>
        /// Get Soil Conservation Submitted Data.
        /// </summary>
        /// <returns> Soil Conservation Submitted Data Details.</returns>
        List<SoilConservationSubmittedDataResponse> GetSoilConservationSubmittedData(int panchayat_id, int scheme_id);

        /// <summary>
        /// Get Soil Conservation Notification.
        /// </summary>
        /// <returns>Get Soil Conservation Notification Data.</returns>
        List<SoilConservationNotificationResponse> GetSoilConservationNotification(string panchayat_id);

        /// <summary>
        /// Get Soil Conservation Notification.
        /// </summary>
        /// <returns>Get Soil Conservation Notification Data.</returns>
        List<SoilConservationExistingPhysicalFinancialTarget> GetExistingPhysicalFinancialTarget(int panchayat_id);

        /// <summary>
        /// Get Post Soil Conservation.
        /// </summary>
        /// <returns> Post Soil Conservation Result </returns>
        bool PostSoilConservation(List<SoilConservationCreateRequest> postSoilConservation);
    }
}
