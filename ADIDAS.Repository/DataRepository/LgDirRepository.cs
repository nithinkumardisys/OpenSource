//------------------------------------------------------------------------------
// <copyright file="LgDirRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Repository.DataRepository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Helper;
    using ADIDAS.Repository.Interfaces;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// LgDirRepository.
    /// </summary>
    public class LgDirRepository : BaseRepository, ILgDirRepository
    {
        private static string qnGetLGDirectoryDistrictData = "GetDistrictData";
        private static string spGetLGDirectoryDistrictData = "usp_ext_cntrlr";
        private static string qnGetLGDirectoryBlockData = "GetBlockDataDist";
        private static string qnGetLGDirectoryBlockDataBlk = "GetBlockDataBlk";
        private static string qnGetLGDirectoryPanchayatData = "GetPanchayatDataDist";
        private static string qnGetLGDirectoryPanchayatDataBlk = "GetPanchayatDataBlk";
        private static string qnGetLGDirectoryPanchayatDataPanch = "GetPanchayatDataPanchyt";

        /// <summary>
        /// Initializes a new instance of the <see cref="LgDirRepository"/> class.
        /// LgDirRepository.
        /// </summary>
        /// <param name="config">config.</param>
        public LgDirRepository(IConfiguration config)
            : base(config)
        {
            SqlHelper.SetConnectionString(this.GetConnectionString());
        }

        /// <summary>
        /// GetLGDirectoryDistrictData.
        /// </summary>
        /// <returns>List.</returns>
        public List<District> GetLGDirectoryDistrictData()
        {
            List<District> list = new List<District>();
            List<DbParameter> sqlParams = new List<DbParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetLGDirectoryDistrictData, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@District_Lg_Code", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@Block_Lg_Code", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetLGDirectoryDistrictData, sqlParams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<District>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetLGDirectoryBlockData.
        /// </summary>
        /// <param name="districtlgCode">districtlgCode.</param>
        /// <returns>List Values.</returns>
        public BlockResponse GetLGDirectoryBlockData(string districtlgCode)
        {
            List<DbParameter> distquery = new List<DbParameter>();
            distquery.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetLGDirectoryBlockData, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            distquery.Add(new SqlParameter { ParameterName = "@District_Lg_Code", Value = districtlgCode, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            distquery.Add(new SqlParameter { ParameterName = "@Block_Lg_Code", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            List<DbParameter> blockquery = new List<DbParameter>();
            blockquery.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetLGDirectoryBlockDataBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            blockquery.Add(new SqlParameter { ParameterName = "@District_Lg_Code", Value = districtlgCode, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            blockquery.Add(new SqlParameter { ParameterName = "@Block_Lg_Code", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dtDistrict = SqlHelper.ExecuteSelect<SqlConnection>(spGetLGDirectoryDistrictData, distquery, SqlHelper.ExecutionType.Procedure);

            DataTable dtBlock = SqlHelper.ExecuteSelect<SqlConnection>(spGetLGDirectoryDistrictData, blockquery, SqlHelper.ExecutionType.Procedure);

            BlockResponse blockResponse = new BlockResponse();

            if (dtDistrict != null && dtDistrict.Rows.Count > 0 && dtBlock != null && dtBlock.Rows.Count > 0)
            {
                blockResponse.District = new DtoLgDistrict();

                District district = SqlHelper.ConvertDataTableToList<District>(dtDistrict).FirstOrDefault();

                List<Block> blockList = SqlHelper.ConvertDataTableToList<Block>(dtBlock);

                blockResponse.District.DistrictCode = district.District_lg_code.ToString();

                blockResponse.District.DistrictName = district.District_name;

                blockResponse.Blocks = blockList.Select(x => new DtoLgBlock { BlockCode = x.Block_lg_code.ToString(), BlockName = x.Block_name }).ToList();
            }

            return blockResponse;
        }

        /// <summary>
        /// GetLGDirectoryPanchayatData.
        /// </summary>
        /// <param name="districtlgCode">districtlgCode.</param>
        /// <param name="blockLgCode">blockLgCode.</param>
        /// <returns>List Values.</returns>
        public PanchayatResponse GetLGDirectoryPanchayatData(string districtlgCode, string blockLgCode)
        {
            List<DbParameter> distquery = new List<DbParameter>();
            distquery.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetLGDirectoryPanchayatData, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            distquery.Add(new SqlParameter { ParameterName = "@District_Lg_Code", Value = districtlgCode, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            distquery.Add(new SqlParameter { ParameterName = "@Block_Lg_Code", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            List<DbParameter> blockquery = new List<DbParameter>();
            blockquery.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetLGDirectoryPanchayatDataBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            blockquery.Add(new SqlParameter { ParameterName = "@District_Lg_Code", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            blockquery.Add(new SqlParameter { ParameterName = "@Block_Lg_Code", Value = blockLgCode, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            List<DbParameter> queryPanchayat = new List<DbParameter>();
            queryPanchayat.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetLGDirectoryPanchayatDataPanch, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            queryPanchayat.Add(new SqlParameter { ParameterName = "@District_Lg_Code", Value = districtlgCode, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            queryPanchayat.Add(new SqlParameter { ParameterName = "@Block_Lg_Code", Value = blockLgCode, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dtDistrict = SqlHelper.ExecuteSelect<SqlConnection>(spGetLGDirectoryDistrictData, distquery, SqlHelper.ExecutionType.Procedure);

            DataTable dtBlock = SqlHelper.ExecuteSelect<SqlConnection>(spGetLGDirectoryDistrictData, blockquery, SqlHelper.ExecutionType.Procedure);

            DataTable dtPanchayat = SqlHelper.ExecuteSelect<SqlConnection>(spGetLGDirectoryDistrictData, queryPanchayat, SqlHelper.ExecutionType.Procedure);

            PanchayatResponse panchayatResponse = new PanchayatResponse();

            if (dtDistrict != null && dtDistrict.Rows.Count > 0)
            {
                if (dtBlock != null && dtBlock.Rows.Count > 0)
                {
                    if (dtPanchayat != null && dtPanchayat.Rows.Count > 0)
                    {
                        District district = SqlHelper.ConvertDataTableToList<District>(dtDistrict).FirstOrDefault();

                        Block block = SqlHelper.ConvertDataTableToList<Block>(dtBlock).FirstOrDefault();

                        List<Panchayat> panchayatList = SqlHelper.ConvertDataTableToList<Panchayat>(dtPanchayat);

                        panchayatResponse.District = new DtoLgDistrict();

                        panchayatResponse.Block = new DtoLgBlock();

                        panchayatResponse.District.DistrictCode = district.District_lg_code.ToString();

                        panchayatResponse.District.DistrictName = district.District_name;

                        panchayatResponse.Block.BlockCode = block.Block_lg_code.ToString();

                        panchayatResponse.Block.BlockName = block.Block_name;

                        panchayatResponse.Panchayats = panchayatList.Select(x => new DtoLgPanchayat { PanchayatCode = x.Panchayat_lg_code.ToString(), PanchayatName = x.Panchayat_name }).ToList();
                    }
                }
            }

            return panchayatResponse;
        }

        /// <summary>
        /// GetLGDirectoryUserForm.
        /// </summary>
        public List<LgDirectoryPanchayatDim> GetLGDirectoryUserForm()
        {
            List<LgDirectoryPanchayatDim> list = new List<LgDirectoryPanchayatDim>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetLGDirectoryUserForm", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Category", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_dim_cntrlr", dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<LgDirectoryPanchayatDim>(dt);
            }

            return list;
        }
    }
}
