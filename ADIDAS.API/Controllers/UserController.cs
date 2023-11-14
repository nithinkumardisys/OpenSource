//------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Service.Interfaces;
    using Azure.Storage.Blobs;
    using DepartmentOfAgriculture.Admin.Models.DTO;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// User Controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IOptions<BlobConfig> config;

        private readonly IUserService userService;

        private readonly ILogger<UserController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// User Controller.
        /// </summary>
        /// <param name="userService">userServiceparam.</param>
        /// <param name="config">config.</param>
        /// <param name="logger">config.</param>
        public UserController(IUserService userService, IOptions<BlobConfig> config, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.config = config;
            this.logger = logger;
        }

        /// <summary>
        /// Get User Details.
        /// </summary>
        /// <param name="userName">userName.</param>
        /// <param name="password">password.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetUserDetails")]
        public IActionResult GetUserInformation(string userName, string password)
        {
            try
            {
                UserEntity result = this.userService.GetUserInformation(userName, password);
                if (result == null)
                {
                    return Unauthorized("Authentication Failed");
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get User Status.
        /// </summary>
        /// <param name="userName">userName.</param>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpGet("GetUserStatus/{UserName}")]
        public IActionResult GetUserStatus(string userName)
        {
            try
            {
                StatusCheck statusCheck = new StatusCheck();
                int result = this.userService.GetUserStatus(userName);
                statusCheck.Status = result;
                return this.Ok(statusCheck);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// Get User Status By PhoneNumber.
        /// </summary>
        /// <param name="phoneNumber">phoneNumber.</param>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpGet("GetUserStatusByPhoneNumber/{PhoneNumber}")]
        public IActionResult GetUserStatusByPhoneNumber(string phoneNumber)
        {
            try
            {
                StatusCheck statusCheck = new StatusCheck();
                int result = this.userService.GetUserStatusbyPhoneNumber(phoneNumber);

                statusCheck.Status = result;

                return this.Ok(statusCheck);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// Get Users Dao.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetUsersDao/{districtId}")]
        public IActionResult GetUsersDao(int districtId)
        {
            try
            {
                DaoDetails result = this.userService.GetUsersDAODetails(districtId);

                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// Update UserStaus For Active Deactive.
        /// </summary>
        /// <param name="status">status.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("UpdateUserStausForActiveDeactive")]
        public IActionResult UpdateUserStausForActiveDeactive([FromBody] DtoUserStatusRequest status)
        {
            try
            {
                int result = this.userService.UpdateUserStausForActiveDeactive(status);

                if (result == 0)
                {
                    return this.NotFound("{\"status\": \"Update Failed\"}");
                }

                return this.Ok("{\"status\": \"Data Successfully Updated\"}");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Update Failed\"}");
            }
        }

        /// <summary>
        /// Get Users Under DAO.
        /// </summary>
        /// <param name="userStatus">userStatus.</param>
        /// <param name="userId">userId.</param>
        /// <returns>Action Result.</returns>
        // [EnableCors("AllowOrigin")]
        [HttpGet("GetUsersUnderDAO/{userStatus}/{userId}")]
        public IActionResult GetUsersUnderDAO(string userStatus, string userId)
        {
            try
            {
                List<DaoUsersDetails> result = this.userService.GetUsersUnderDAO(userStatus, Convert.ToInt32(userId));

                if (result == null)
                {
                    return Unauthorized("Authentication Failed");
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Advanced Search Data.
        /// </summary>
        /// <param name="advancedSearchData">advancedSearchData.</param>
        /// <returns>Action Result.</returns>
        // [EnableCors("AllowOrigin")]
        [HttpPost("GetAdvancedSearchData")]
        public IActionResult GetAdvancedSearchData([FromBody] AdvancedSearchModel advancedSearchData)
        {
            try
            {
                List<DaoUsersDetails> result = this.userService.GetAdvancedSearchData(advancedSearchData);
                if (result == null)
                {
                    return this.NotFound("Not Found");
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get User Privilage.
        /// </summary>
        /// <param name="userName">userName.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetUserPrivilage")]
        public IActionResult GetUserPrivilage(string userName)
        {
            try
            {
                List<UserPrivilege> result = this.userService.GetUserPrivilage(userName);
                if (result == null)
                {
                    return this.NotFound("Not Found");
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get User Details ById.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetUserDetailsById/{userId}")]
        public IActionResult GetUserDetailsById(string userId)
        {
            try
            {
                DaoUsersDetails result = this.userService.GetUserDetailsById(userId);
                if (result == null)
                {
                    return this.NotFound("Not Found");
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        [HttpGet("GetUserDetailsByIdSDO/{UserId}")]
        public IActionResult GetUserDetailsByIdSDO(string UserId)
        {
            DaoUsersDetails result = this.userService.GetUserDetailsByIdSDO(UserId);
            if (result == null)
            {
                return this.NotFound("Not Found");
            }

            return this.Ok(result);
        }

        /// <summary>
        /// Get User Details ByName.
        /// </summary>
        /// <param name="userName">userName.</param>
        /// <param name="userId">userId.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetUserDetailsByName/{UserName}/{userId}")]
        public IActionResult GetUserDetailsByName(string userName, string userId)
        {
            try
            {
                List<UsersInfoDto> result = this.userService.GetUsersByName(userName, userId);
                if (!result.Any())
                {
                    return this.NotFound("Not Found");
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get User Details ByName For AllDesignation.
        /// </summary>
        /// <param name="userName">userName.</param>
        /// <param name="userId">userId.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetUserDetailsByNameForAllDesignation/{UserName}/{userId}")]
        public IActionResult GetUserDetailsByNameForAllDesignation(string userName, string userId)
        {
            try
            {
                List<UsersInfoDto> result = this.userService.GetUsersByNameForAllDesignations(userName, userId);
                if (!result.Any())
                {
                    return this.NotFound("Not Found");
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Permissions.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpGet("GetPermissions")]
        public IActionResult GetPermissions()
        {
            try
            {
                PermissionsModel result = this.userService.GetPermissions();
                if (result == null)
                {
                    return this.NotFound("Not Found");
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Role Details.
        /// </summary>
        /// <param name="roleId">roleId.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetRoleDetails/{RoleId}")]
        public IActionResult GetRoleDetails(string roleId)
        {
            try
            {
                DtoRoleInfo result = this.userService.GetRoleDetails(roleId);
                if (result == null)
                {
                    return this.NotFound("Not Found");
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Role List.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpGet("GetRoleList")]
        public IActionResult GetRoleList()
        {
            try
            {
                List<RoleList> result = this.userService.GetRolesList();
                if (!result.Any())
                {
                    return this.NotFound("Not Found");
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get User Role List.
        /// </summary>
        /// <param name="roleId">roleId.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetUserRoleList/{RoleId}")]
        public IActionResult GetUserRoleList(string roleId)
        {
            try
            {
                List<DtoUserList> result = this.userService.GetUserRoleList(roleId);
                if (!result.Any())
                {
                    return this.NotFound("Not Found");
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Post User Details.
        /// </summary>
        /// <param name="user">user.</param>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("PostUserDetails")]
        public IActionResult PostUserDetails([FromBody] UserEntity user)
        {
            try
            {
                UserEntity result = this.userService.PostUserDetails(user);

                if (result == null)
                {
                    return Unauthorized(result);
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Edit User Info.
        /// </summary>
        /// <param name="user">user.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("EditUserInfo")]
        public IActionResult EditUserInfo([FromBody] EditUserModel user)
        {
            try
            {
                AppUserName result = this.userService.EditUserInfo(user);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Check Conflict On Status Update.
        /// </summary>
        /// <param name="userStatus">userStatus.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("CheckConflictOnStatusUpdate")]
        public IActionResult CheckConflictOnStatusUpdate([FromBody] DtoUserStatus userStatus)
        {
            try
            {
                string result = this.userService.CheckConflictonStatusUpdate(userStatus);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Save User Profile.
        /// </summary>
        /// <param name="user">user.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("SaveUserProfile")]
        public async Task<IActionResult> SaveUserProfile([FromBody] DtoUserProfile user)
        {
            try
            {
                DtoUserProfile result = await this.userService.SaveUserProfile(user);

                if (result != null)
                {
                    return this.Ok(result);
                }

                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// Save User Personal Information.
        /// </summary>
        /// <param name="user">user.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("SaveUserPersonalInformation")]
        public IActionResult SaveUserPersonalInformation([FromBody] DtoUserProfile user)
        {
            try
            {
                int result = this.userService.SaveUserPersonalInformation(user);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// Update User Status.
        /// </summary>
        /// <param name="status">status.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("UpdateUserStatus")]
        public IActionResult UpdateUserStatus([FromBody] DtoUserStatusRequest status)
        {
            try
            {
                List<AppUserName> result = this.userService.UpdateUserStaus(status);

                if (!result.Any())
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// Approve Users.
        /// </summary>
        /// <param name="status">status.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("ApproveUsers")]
        public IActionResult ApproveUsers([FromBody] ApproveUserStatusRequest status)
        {
            try
            {
                List<AppUserName> result = this.userService.ApproveUsers(status);

                if (!result.Any())
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// Post User Info.
        /// </summary>
        /// <param name="userDetails">userDetails.</param>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("PostUserInfo")]
        public async Task<IActionResult> PostUserInfo([FromBody] UserDetails userDetails)
        {
            try
            {
                string userId = this.userService.InsertUserInfo(userDetails);

                if (!string.IsNullOrEmpty(userId))
                {
                    if (!string.IsNullOrEmpty(userDetails.ImageData))
                    {
                        BlobEntity blobEntity = new BlobEntity();
                        blobEntity.DirectoryName = "Profile";
                        blobEntity.FolderName = userDetails.FirstName + "-" + userId + "-" + DateTime.Now.ToString("dd-MM-yyyy") + ".jpg";
                        blobEntity.ByteArray = userDetails.ImageData;

                        BlobServiceClient blobServiceClient = new BlobServiceClient(this.config.Value.BlobConnection);

                        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("mobileapp");

                        string blobPath = blobEntity.DirectoryName + Path.AltDirectorySeparatorChar + blobEntity.FolderName;

                        BlobClient blobClient = containerClient.GetBlobClient(blobPath);

                        byte[] bytes1 = Convert.FromBase64String(blobEntity.ByteArray);
                        Stream stream = new MemoryStream(bytes1);

                        await blobClient.UploadAsync(stream, true);
                        this.userService.UpdateImageDetails(blobEntity.FolderName, this.config.Value.UserProfilePhoto, userId);
                    }
                    else
                    {
                        this.userService.UpdateImageDetails("default.png", this.config.Value.UserProfilePhoto, userId);
                    }

                    return this.Ok("{\"status\": \"Data Successfully Inserted\"}");
                }
                else
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Insert Role Details.
        /// </summary>
        /// <param name="roleDetails">roleDetails.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("InsertRoleDetails")]
        public IActionResult InsertRoleDetails([FromBody] UserRoleModel roleDetails)
        {
            try
            {
                int roleId = this.userService.InsertRoleDetails(roleDetails);

                if (roleId > 0)
                {
                    return this.Ok("{\"status\": \"Data Successfully Inserted\"}");
                }
                else if (roleId == -1)
                {
                    return this.Ok("{\"status\": \"Data Conflict\"}");
                }

                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Update Role Status.
        /// </summary>
        /// <param name="roleStatus">roleStatus.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("UpdateRoleStatus")]
        public IActionResult UpdateRoleStatus([FromBody] RoleStatus roleStatus)
        {
            try
            {
                int status = this.userService.UpdateRoleStatus(roleStatus.RoleId.ToString(), roleStatus.Status);

                if (status == 1)
                {
                    return this.Ok("{\"status\": \"Data Successfully Inserted\"}");
                }

                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Update User Password.
        /// </summary>
        /// <param name="userPassword">userPassword.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("UpdateUserPassword/{userPassword}")]
        public IActionResult UpdateUserPassword(string userPassword)
        {
            try
            {
                int status = this.userService.ResetUserPassword(userPassword);

                if (status == 1)
                {
                    return this.Ok();
                }

                return this.NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// Delete Role.
        /// </summary>
        /// <param name="roleId">roleId.</param>
        /// <returns>Action Result.</returns>
        [HttpDelete("DeleteRole/{RoleId}")]
        public IActionResult DeleteRole(string roleId)
        {
            try
            {
                int status = this.userService.DeleteRoles(roleId);

                if (status == 1)
                {
                    return this.Ok("{\"status\": \"Data Successfully Deleted\"}");
                }

                return this.NotFound("{\"status\": \"Deletion Failed\"}");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Delete Group.
        /// </summary>
        /// <param name="groupId">groupId.</param>
        /// <param name="status">status.</param>
        /// <returns>Action Result.</returns>
        [HttpDelete("DeleteGroup/{GroupId}/{Status}")]
        public IActionResult DeleteGroup(string groupId, string status)
        {
            try
            {
                int status1 = this.userService.DeleteGroup(groupId, status);

                if (status1 == 1)
                {
                    return this.Ok("{\"status\": \"Data Successfully Deleted\"}");
                }

                return this.NotFound("{\"status\": \"Deletion Failed\"}");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Update Group Status.
        /// </summary>
        /// <param name="status">status.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("UpdateGroupStatus")]
        public IActionResult UpdateGroupStatus([FromBody] DtoGroupStatus status)
        {
            try
            {
                var result = this.userService.UpdateGroupStatus(status);

                if (result == 0)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// Send Messages.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpPost("SendMessages")]
        public IActionResult SendMessages()
        {
            try
            {
                var result = this.userService.SendMessage();

                if (result == 0)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// Post Group Details.
        /// </summary>
        /// <param name="userGroupModel">userGroupModel.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("PostGroupDetails")]
        public IActionResult PostGroupDetails([FromBody] UserGroupModel userGroupModel)
        {
            try
            {
                var result = this.userService.InsertGroupDetails(userGroupModel);
                if (result > 0)
                {
                    return this.Ok("{\"status\": \"Data Successfully Inserted\"}");
                }
                else if (result == -1)
                {
                    return this.Ok("{\"status\": \"Data Conflict\"}");
                }

                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// On Edit Group Details.
        /// </summary>
        /// <param name="userGroupModel">userGroupModel.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("OnEditGroupDetails")]
        public IActionResult OnEditGroupDetails([FromBody] UserGroupModel userGroupModel)
        {
            try
            {
                var result = this.userService.InsertGroupDetails(userGroupModel);
                if (result > 0)
                {
                    return this.Ok("{\"status\": \"Data Successfully Updated\"}");
                }

                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// Get Group List.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpGet("GetGroupList")]
        public IActionResult GetGroupList()
        {
            try
            {
                List<DtoGroupList> result = this.userService.GetGroupList();
                if (!result.Any())
                {
                    return this.NotFound("Not Found");
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Sender User List.
        /// </summary>
        /// <param name="groupId">groupId.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetSenderUserList/{groupId}")]
        public IActionResult GetSenderUserList(string groupId)
        {
            try
            {
                List<DtoSenderUserList> result = this.userService.GetSenderUserList(groupId);
                if (!result.Any())
                {
                    return this.NotFound("Not Found");
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Recipient User List.
        /// </summary>
        /// <param name="groupId">groupId.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetRecipientUserList/{groupId}")]
        public IActionResult GetRecipientUserList(string groupId)
        {
            try
            {
                List<DtoRecipientUserList> result = this.userService.GetRecipientsUserList(groupId);
                if (!result.Any())
                {
                    return this.NotFound("Not Found");
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Group Details.
        /// </summary>
        /// <param name="groupId">groupId.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetGroupDetails/{groupId}")]
        public IActionResult GetGroupDetails(string groupId)
        {
            try
            {
                var result = this.userService.GetGroupDetails(groupId);
                if (result != null)
                {
                    return this.Ok(result);
                }

                return this.NotFound("Not Found");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Transfer History.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetTransferHistory/{UserId}")]
        public IActionResult GetTransferHistory(string userId)
        {
            try
            {
                var result = this.userService.GetTransferHistory(userId);
                if (result != null)
                {
                    return this.Ok(result);
                }

                return this.NotFound("Not Found");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Conflict Users.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetConflictUsers/{UserId}")]
        public IActionResult GetConflictUsers(string userId)
        {
            try
            {
                var result = this.userService.GetConflictUsers(userId);
                if (result != null)
                {
                    return this.Ok(result);
                }

                return this.NotFound("Not Found");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Insert User Transfer.
        /// </summary>
        /// <param name="transferUsers">transferUsers.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("InsertUserTransfer")]
        public IActionResult InsertUserTransfer([FromBody] List<TransferUser> transferUsers)
        {
            try
            {
                var result = this.userService.InsertTransfer(transferUsers);
                if (result != 0)
                {
                    return this.Ok(result);
                }

                return this.NotFound("Not Found");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Save Notification Token.
        /// </summary>
        /// <param name="notification">notification.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("SaveNotificationToken")]
        public IActionResult SaveNotificationToken([FromBody] PushNotificationInfo notification)
        {
            try
            {
                var result = this.userService.SaveNotificationToken(notification);
                if (result != string.Empty)
                {
                    return this.Ok(result);
                }

                return this.NotFound("Not Found");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Send Push Notification.
        /// </summary>
        /// <param name="notify">notify.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("SendPushNotification")]
        public IActionResult SendNotification([FromBody] NotificationItem notify)
        {
            try
            {
                var result = this.userService.SendNotification(notify);
                if (result.Any())
                {
                    return this.Ok(result);
                }

                return this.NotFound("Not Found");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Marque Messsage.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpGet("MarqueMesssage")]
        public IActionResult MarqueMesssage()
        {
            try
            {
                var result = this.userService.MarqueMesssage();
                if (result.Any())
                {
                    return this.Ok("{\"status\": \"" + result + "\"}");
                }

                return this.NotFound("Not Found");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }
    }
}
