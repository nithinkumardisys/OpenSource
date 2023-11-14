//------------------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using DepartmentOfAgriculture.Admin.Models.DTO;

    /// <summary>
    /// IUserRepository.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// GetUserInformation.
        /// </summary>
        /// <param name="userName">userName.</param>
        /// <param name="password">password.</param>
        /// <returns>UserEntity.</returns>
        UserEntity GetUserInformation(string userName, string password);

        /// <summary>
        /// GetUserPrivilage.
        /// </summary>
        /// <param name="userName">UserPrivilege.</param>
        /// <returns>UserPrivilege Result.</returns>
        List<UserPrivilege> GetUserPrivilage(string userName);

        /// <summary>
        /// PostUserDetails.
        /// </summary>
        /// <param name="user">user.</param>
        /// <returns>UserEntity.</returns>
        UserEntity PostUserDetails(UserEntity user);

        /// <summary>
        /// GetUserStatus.
        /// </summary>
        /// <param name="username">username.</param>
        /// <returns>Values.</returns>
        int GetUserStatus(string username);

        /// <summary>
        /// InsertUserInfo.
        /// </summary>
        /// <param name="userDetails">userDetails.</param>
        /// <returns>Response.</returns>
        string InsertUserInfo(UserDetails userDetails);

        /// <summary>
        /// GetUsersDAODetails.
        /// </summary>
        /// <param name="districtid">districtid.</param>
        /// <returns>DaoDetails.</returns>
        DaoDetails GetUsersDAODetails(int districtid);

        /// <summary>
        /// SendMessage.
        /// </summary>
        /// <returns>Sending Mail and Response.</returns>
        int SendMessage();

        /// <summary>
        /// UpdateImageDetails.
        /// </summary>
        /// <param name="filename">filename.</param>
        /// <param name="filelocation">filelocation.</param>
        /// <param name="userid">userid.</param>
        /// <returns>Update Response.</returns>
        int UpdateImageDetails(string filename, string filelocation, string userid);

        /// <summary>
        /// UpdateUserStaus.
        /// </summary>
        /// <param name="userStatus">userStatus.</param>
        /// <returns>AppUserName.</returns>
        List<AppUserName> UpdateUserStaus(DtoUserStatusRequest userStatus);

        /// <summary>
        /// GetUserDetailsById.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <param name="queryname">queryname.</param>
        /// <returns>DaoUsersDetails.</returns>
        DaoUsersDetails GetUserDetailsById(string userId, string queryname);

        /// <summary>
        /// GetUsersUnderDAO.
        /// </summary>
        /// <param name="userStatus">userStatus.</param>
        /// <param name="userID">userID.</param>
        /// <returns>DaoUsersDetails.</returns>
        List<DaoUsersDetails> GetUsersUnderDAO(string userStatus, int userID);

        /// <summary>
        /// GetAdvancedSearchData.
        /// </summary>
        /// <param name="advancedSearchModel">advancedSearchModel.</param>
        /// <returns>DaoUsersDetails.</returns>
        List<DaoUsersDetails> GetAdvancedSearchData(AdvancedSearchModel advancedSearchModel);

        /// <summary>
        /// EditUserInfo.
        /// </summary>
        /// <param name="edituserDetails">edituserDetails.</param>
        /// <returns>AppUserName.</returns>
        AppUserName EditUserInfo(EditUserModel edituserDetails);

        /// <summary>
        /// UpdateUserStausForActiveDeactive.
        /// </summary>
        /// <param name="userStatus">userStatus.</param>
        /// <returns>Response.</returns>
        int UpdateUserStausForActiveDeactive(DtoUserStatusRequest userStatus);

        /// <summary>
        /// GetUsersByName.
        /// </summary>
        /// <param name="userName">userName.</param>
        /// <param name="userId">userId.</param>
        /// <returns>UsersInfoDTO.</returns>
        List<UsersInfoDto> GetUsersByName(string userName, string userId);

        /// <summary>
        /// GetPermissions.
        /// </summary>
        /// <returns>PermissionsModel.</returns>
        PermissionsModel GetPermissions();

        /// <summary>
        /// InsertRoleDetails.
        /// </summary>
        /// <param name="roleDetails">roleDetails.</param>
        /// <returns>Response.</returns>
        int InsertRoleDetails(UserRoleModel roleDetails);

        /// <summary>
        /// GetRoleDetails.
        /// </summary>
        /// <param name="roleId">roleId.</param>
        /// <returns>DtoRoleInfo.</returns>
        DtoRoleInfo GetRoleDetails(string roleId);

        /// <summary>
        /// GetRolesList.
        /// </summary>
        /// <returns>RoleList.</returns>
        List<RoleList> GetRolesList();

        /// <summary>
        /// UpdateRoleStatus.
        /// </summary>
        /// <param name="roleId">roleId.</param>
        /// <param name="status">status.</param>
        /// <returns>Response.</returns>
        int UpdateRoleStatus(string roleId, string status);

        /// <summary>
        /// DeleteRoles.
        /// </summary>
        /// <param name="roleId">roleId.</param>
        /// <returns>Response.</returns>
        int DeleteRoles(string roleId);

        /// <summary>
        /// DeleteGroup.
        /// </summary>
        /// <param name="groupId">groupId.</param>
        /// <param name="status">status.</param>
        /// <returns>Response.</returns>
        int DeleteGroup(string groupId, string status);

        /// <summary>
        /// GetUserRoleList.
        /// </summary>
        /// <param name="roleId">roleId.</param>
        /// <returns>DtoUserList.</returns>
        List<DtoUserList> GetUserRoleList(string roleId);

        /// <summary>
        /// InsertGroupDetails.
        /// </summary>
        /// <param name="userGroupModel">userGroupModel.</param>
        /// <returns>Response.</returns>
        int InsertGroupDetails(UserGroupModel userGroupModel);

        /// <summary>
        /// GetGroupList.
        /// </summary>
        /// <returns>DtoGroupList.</returns>
        List<DtoGroupList> GetGroupList();

        /// <summary>
        /// GetSenderUserList.
        /// </summary>
        /// <param name="groupId">groupId.</param>
        /// <returns>DtoSenderUserList.</returns>
        List<DtoSenderUserList> GetSenderUserList(string groupId);

        /// <summary>
        /// GetGroupDetails.
        /// </summary>
        /// <param name="groupID">groupID.</param>
        /// <returns>DtoGroupModel.</returns>
        DtoGroupModel GetGroupDetails(string groupID);

        /// <summary>
        /// UpdateGroupStatus.
        /// </summary>
        /// <param name="groupStatus">groupStatus.</param>
        /// <returns>Response.</returns>
        int UpdateGroupStatus(DtoGroupStatus groupStatus);

        /// <summary>
        /// GetRecipientsUserList.
        /// </summary>
        /// <param name="groupId">groupId.</param>
        /// <returns>DtoRecipientUserList.</returns>
        List<DtoRecipientUserList> GetRecipientsUserList(string groupId);

        /// <summary>
        /// GetUsersByNameForAllDesignations.
        /// </summary>
        /// <param name="userName">userName.</param>
        /// <param name="userId">userId.</param>
        /// <returns>UsersInfoDTO.</returns>
        List<UsersInfoDto> GetUsersByNameForAllDesignations(string userName, string userId);

        /// <summary>
        /// SaveUserProfile.
        /// </summary>
        /// <param name="userProfile">userProfile.</param>
        /// <returns>DtoUserProfile.</returns>
        Task<DtoUserProfile> SaveUserProfile(DtoUserProfile userProfile);

        /// <summary>
        /// GetUserStatusbyPhoneNumber.
        /// </summary>
        /// <param name="phoneNumber">phoneNumber.</param>
        /// <returns>List.</returns>
        int GetUserStatusbyPhoneNumber(string phoneNumber);

        /// <summary>
        /// CheckConflictonStatusUpdate.
        /// </summary>
        /// <param name="userStatus">userStatus.</param>
        /// <returns>Status Update.</returns>
        string CheckConflictonStatusUpdate(DtoUserStatus userStatus);

        /// <summary>
        /// InsertTransfer.
        /// </summary>
        /// <param name="transferUsers">transferUsers.</param>
        /// <returns>Response.</returns>
        int InsertTransfer(List<TransferUser> transferUsers);

        /// <summary>
        /// GetTransferHistory.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>TransferHistory.</returns>
        List<TransferHistory> GetTransferHistory(string userId);

        /// <summary>
        /// SaveUserPersonalInformation.
        /// </summary>
        /// <param name="userProfile">userProfile.</param>
        /// <returns>Response.</returns>
        int SaveUserPersonalInformation(DtoUserProfile userProfile);

        /// <summary>
        /// ApproveUsers.
        /// </summary>
        /// <param name="userStatus">userStatus.</param>
        /// <returns>AppUserName.</returns>
        List<AppUserName> ApproveUsers(ApproveUserStatusRequest userStatus);

        /// <summary>
        /// GetConflictUsers.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>ConflictUsers.</returns>
        List<ConflictUsers> GetConflictUsers(string userId);

        /// <summary>
        /// ResetUserPassword.
        /// </summary>
        /// <param name="userName">userName.</param>
        /// <returns>Response.</returns>
        int ResetUserPassword(string userName);

        /// <summary>
        /// SaveRefreshToken.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <param name="refreshToken">refreshToken.</param>
        /// <param name="refreshTokenExpiryTime">refreshTokenExpiryTime.</param>
        /// <returns>Response.</returns>
        int SaveRefreshToken(string userId, string refreshToken, DateTime refreshTokenExpiryTime);

        /// <summary>
        /// GetToken.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>TokenData.</returns>
        TokenData GetToken(string userId);

        /// <summary>
        /// SaveNotificationToken.
        /// </summary>
        /// <param name="notification">notification.</param>
        /// <returns>Response.</returns>
        string SaveNotificationToken(PushNotificationInfo notification);

        /// <summary>
        /// SendNotification.
        /// </summary>
        /// <param name="notify">notify.</param>
        /// <returns>NotificationResponse.</returns>
        List<NotificationResponse> SendNotification(NotificationItem notify);

        /// <summary>
        /// MarqueMesssage.
        /// </summary>
        /// <returns>Values.</returns>
        string MarqueMesssage();
    }
}
