//------------------------------------------------------------------------------
// <copyright file="LgDirService.cs" company="Government of Bihar">
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
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.Interfaces;

    public class LgDirService : ILgDirService
    {
        private readonly ILgDirRepository lgDirRepository;

        public LgDirService(ILgDirRepository lgDirRepository)
        {
            this.lgDirRepository = lgDirRepository;
        }

        public List<District> GetLGDirectoryDistrictData()
        {
            return this.lgDirRepository.GetLGDirectoryDistrictData();
        }

        public BlockResponse GetLGDirectoryBlockData(string districtlgCode)
        {
            return this.lgDirRepository.GetLGDirectoryBlockData(districtlgCode);
        }

        public PanchayatResponse GetLGDirectoryPanchayatData(string districtlgCode, string blockLgCode)
        {
            return this.lgDirRepository.GetLGDirectoryPanchayatData(districtlgCode, blockLgCode);
        }

        /*public VillageResponse GetLGDirectoryVillageData(string districtlgCode, string blockLgCode, string panchayatCode)
        {
            return this.lgDirRepository.GetLGDirectoryVillageData(districtlgCode, blockLgCode, panchayatCode);
        }*/

        public List<LgDirectoryPanchayatDim> GetLGDirectoryUserForm()
        {
            return this.lgDirRepository.GetLGDirectoryUserForm();
        }
    }
}
