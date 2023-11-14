//------------------------------------------------------------------------------
// <copyright file="IUserService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Service.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using DepartmentOfAgriculture.Admin.Models.DTO;

    /// <summary>
    /// IUserService.
    /// </summary>
    public interface IUserService
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
        /// <param name="userName">userName.</param>
        /// <returns>UserPrivilege.</returns>
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
        /// <returns>integer.</returns>
        int GetUserStatus(string username);

        /// <summary>
        /// GetUserStatusbyPhoneNumber.
        /// </summary>
        /// <param name="phoneNumber">phoneNumber.</param>
        /// <returns>integer.</returns>
        int GetUserStatusbyPhoneNumber(string phoneNumber);

        /// <summary>
        /// InsertUserInfo.
        /// </summary>
        /// <param name="userDetails">userDetails.</param>
        /// <returns>string.</returns>
        string InsertUserInfo(UserDetails userDetails);

        /// <summary>
        /// UpdateImageDetails.
        /// </summary>
        /// <param name="filename">filename.</param>
        /// <param name="filelocation">filelocation.</param>
        /// <param name="userid">userid.</param>
        /// <returns>integer.</returns>
        int UpdateImageDetails(string filename, string filelocation, string userid);

        /// <summary>
        /// GetUsersDAODetails.
        /// </summary>
        /// <param name="districtid">districtid.</param>
        /// <returns>DaoDetails.</returns>
        DaoDetails GetUsersDAODetails(int districtid);

        /// <summary>
        /// UpdateUserStaus.
        /// </summary>
        /// <param name="userStatus">userStatus.</param>
        /// <returns>AppUserName list.</returns>
        List<AppUserName> UpdateUserStaus(DtoUserStatusRequest userStatus);

        /// <summary>
        /// GetUserDetailsByIdSDO.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>DaoUsersDetails.</returns>
        DaoUsersDetails GetUserDetailsByIdSDO(string userId);

        /// <summary>
        /// GetUserDetailsById.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>DaoUsersDetails.</returns>
        DaoUsersDetails GetUserDetailsById(string userId);

        /// <summary>
        /// GetUsersUnderDAO.
        /// </summary>
        /// <param name="userStatus">userStatus.</param>
        /// <param name="userID">userID.</param>
        /// <returns>DaoUsersDetails list.</returns>
        List<DaoUsersDetails> GetUsersUnderDAO(string userStatus, int userID);

        /// <summary>
        /// GetAdvancedSearchData.
        /// </summary>
        /// <param name="userDetails">userDetails.</param>
        /// <returns>DaoUsersDetails list.</returns>
        List<DaoUsersDetails> GetAdvancedSearchData(AdvancedSearchModel userDetails);

        /// <summary>
        /// SendMessage.
        /// </summary>
        /// <returns>integer.</returns>
        int SendMessage();

        /// <summary>
        /// EditUserInfo.
        /// </summary>
        /// <param name="edituserDetails">edituserDetails.</param>
        /// <returns>AppUserName.</returns>
        AppUserName EditUserInfo(EditUserModel edituserDetails);

        /// <summary>
        /// GetUsersByName.
        /// </summary>
        /// <param name="userName">userName.</param>
        /// <param name="userId">userId.</param>
        /// <returns>UsersInfoDto list.</returns>
        List<UsersInfoDto> GetUsersByName(string userName, string userId);

        /// <summary>
        /// UpdateUserStausForActiveDeactive.
        /// </summary>
        /// <param name="userStatus">userStatus.</param>
        /// <returns>integer.</returns>
        int UpdateUserStausForActiveDeactive(DtoUserStatusRequest userStatus);

        /// <summary>
        /// GetPermissions.
        /// </summary>
        /// <returns>PermissionsModel.</returns>
        PermissionsModel GetPermissions();

        /// <summary>
        /// InsertRoleDetails.
        /// </summary>
        /// <param name="roleDetails">roleDetails.</param>
        /// <returns>integer.</returns>
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
        /// <returns>RoleList list.</returns>
        List<RoleList> GetRolesList();

        /// <summary>
        /// UpdateRoleStatus.
        /// </summary>
        /// <param name="roleId">roleId.</param>
        /// <param name="status">status.</param>
        /// <returns>integer.</returns>
        int UpdateRoleStatus(string roleId, string status);

        /// <summary>
        /// DeleteRoles.
        /// </summary>
        /// <param name="roleId">roleId.</param>
        /// <returns>integer.</returns>
        int DeleteRoles(string roleId);

        /// <summary>
        /// DeleteGroup.
        /// </summary>
        /// <param name="groupId">groupId.</param>
        /// <param name="status">status.</param>
        /// <returns>integer.</returns>
        int DeleteGroup(string groupId, string status);

        /// <summary>
        /// GetUserRoleList.
        /// </summary>
        /// <param name="roleId">roleId.</param>
        /// <returns>DtoUserList list.</returns>
        List<DtoUserList> GetUserRoleList(string roleId);

        /// <summary>
        /// UpdateGroupStatus.
        /// </summary>
        /// <param name="groupStatus">groupStatus.</param>
        /// <returns>integer.</returns>
        int UpdateGroupStatus(DtoGroupStatus groupStatus);

        /// <summary>
        /// GetGroupList.
        /// </summary>
        /// <returns>DtoGroupList list.</returns>
        List<DtoGroupList> GetGroupList();

        /// <summary>
        /// GetSenderUserList.
        /// </summary>
        /// <param name="groupId">groupId.</param>
        /// <returns>DtoSenderUserList list.</returns>
        List<DtoSenderUserList> GetSenderUserList(string groupId);

        /// <summary>
        /// InsertGroupDetails.
        /// </summary>
        /// <param name="userGroupDetails">userGroupDetails.</param>
        /// <returns>integer.</returns>
        int InsertGroupDetails(UserGroupModel userGroupDetails);

        /// <summary>
        /// GetGroupDetails.
        /// </summary>
        /// <param name="groupID">groupID.</param>
        /// <returns>DtoGroupModel.</returns>
        DtoGroupModel GetGroupDetails(string groupID);

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
        /// <returns>UsersInfoDto.</returns>
        List<UsersInfoDto> GetUsersByNameForAllDesignations(string userName, string userId);

        /// <summary>
        /// SaveUserProfile.
        /// </summary>
        /// <param name="userProfile">userProfile.</param>
        /// <returns>DtoUserProfile.</returns>
        Task<DtoUserProfile> SaveUserProfile(DtoUserProfile userProfile);

        /// <summary>
        /// CheckConflictonStatusUpdate.
        /// </summary>
        /// <param name="userStatus">userStatus.</param>
        /// <returns>string.</returns>
        string CheckConflictonStatusUpdate(DtoUserStatus userStatus);

        /// <summary>
        /// InsertTransfer.
        /// </summary>
        /// <param name="transferUsers">transferUsers.</param>
        /// <returns>integer.</returns>
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
        /// <returns>integer.</returns>
        int SaveUserPersonalInformation(DtoUserProfile userProfile);

        /// <summary>
        /// ApproveUsers.
        /// </summary>
        /// <param name="userStatus">userStatus.</param>
        /// <returns>AppUserName list.</returns>
        List<AppUserName> ApproveUsers(ApproveUserStatusRequest userStatus);

        /// <summary>
        /// GetConflictUsers.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>ConflictUsers list.</returns>
        List<ConflictUsers> GetConflictUsers(string userId);

        /// <summary>
        /// ResetUserPassword.
        /// </summary>
        /// <param name="userName">userName.</param>
        /// <returns>integer.</returns>
        int ResetUserPassword(string userName);

        /// <summary>
        /// SaveRefreshToken.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <param name="refreshToken">refreshToken.</param>
        /// <param name="refreshTokenExpiryTime">refreshTokenExpiryTime.</param>
        /// <returns>integer.</returns>
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
        /// <returns>string.</returns>
        string SaveNotificationToken(PushNotificationInfo notification);

        /// <summary>
        /// SendNotification.
        /// </summary>
        /// <param name="notify">notify.</param>
        /// <returns>NotificationResponse list.</returns>
        List<NotificationResponse> SendNotification(NotificationItem notify);

        /// <summary>
        /// MarqueMesssage.
        /// </summary>
        /// <returns>string.</returns>
        string MarqueMesssage();
    }
}
