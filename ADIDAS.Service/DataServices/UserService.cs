//------------------------------------------------------------------------------
// <copyright file="UserService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Service.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.Interfaces;
    using DepartmentOfAgriculture.Admin.Models.DTO;

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserEntity GetUserInformation(string userName, string password)
        {
            return this.userRepository.GetUserInformation(userName, password);
        }

        public List<UserPrivilege> GetUserPrivilage(string userName)
        {
            return userRepository.GetUserPrivilage(userName);
        }

        public DaoDetails GetUsersDAODetails(int districtid)
        {
            return userRepository.GetUsersDAODetails(districtid);
        }

        public int GetUserStatus(string username)
        {
            return this.userRepository.GetUserStatus(username);
        }

        public string InsertUserInfo(UserDetails userDetails)
        {
            return this.userRepository.InsertUserInfo(userDetails);
        }

        public UserEntity PostUserDetails(UserEntity user)
        {
            return userRepository.PostUserDetails(user);
        }

        public int UpdateImageDetails(string filename, string filelocation, string userid)
        {
            return userRepository.UpdateImageDetails(filename, filelocation, userid);
        }

        public List<AppUserName> UpdateUserStaus(DtoUserStatusRequest userStatus)
        {
            return userRepository.UpdateUserStaus(userStatus);
        }

        public List<DaoUsersDetails> GetUsersUnderDAO(string userStatus, int userID)
        {
            return userRepository.GetUsersUnderDAO(userStatus, userID);
        }

        public DaoUsersDetails GetUserDetailsById(string userId)
        {
            return userRepository.GetUserDetailsById(userId, "GetUserDetailsById");
        }

        public DaoUsersDetails GetUserDetailsByIdSDO(string UserId)
        {
            return userRepository.GetUserDetailsById(UserId, "GetUserDetailsByIdforSDO");
        }

        public List<DaoUsersDetails> GetAdvancedSearchData(AdvancedSearchModel userDetails)
        {
            return userRepository.GetAdvancedSearchData(userDetails);
        }

        public AppUserName EditUserInfo(EditUserModel edituserDetails)
        {
            return userRepository.EditUserInfo(edituserDetails);
        }

        public int UpdateUserStausForActiveDeactive(DtoUserStatusRequest userStatus)
        {
            return userRepository.UpdateUserStausForActiveDeactive(userStatus);
        }

        public List<UsersInfoDto> GetUsersByName(string userName, string userId)
        {
            return userRepository.GetUsersByName(userName, userId);
        }

        public PermissionsModel GetPermissions()
        {
            return userRepository.GetPermissions();
        }

        public int InsertRoleDetails(UserRoleModel roleDetails)
        {
            return userRepository.InsertRoleDetails(roleDetails);
        }

        public DtoRoleInfo GetRoleDetails(string roleId)
        {
            return userRepository.GetRoleDetails(roleId);
        }

        public List<RoleList> GetRolesList()
        {
            return userRepository.GetRolesList();
        }

        public int UpdateRoleStatus(string roleId, string status)
        {
            return userRepository.UpdateRoleStatus(roleId, status);
        }

        public int DeleteRoles(string roleId)
        {
            return userRepository.DeleteRoles(roleId);
        }

        public int DeleteGroup(string groupId, string status)
        {
            return userRepository.DeleteGroup(groupId, status);
        }

        public List<DtoUserList> GetUserRoleList(string roleId)
        {
            return userRepository.GetUserRoleList(roleId);
        }

        public int UpdateGroupStatus(DtoGroupStatus groupStatus)
        {
            return userRepository.UpdateGroupStatus(groupStatus);
        }

        public DtoGroupModel GetGroupDetails(string groupID)
        {
            return userRepository.GetGroupDetails(groupID);
        }

        public int InsertGroupDetails(UserGroupModel userGroupDetails)
        {
            return userRepository.InsertGroupDetails(userGroupDetails);
        }

        public int UpdateGroupDetails(UserGroupModel userGroupDetails)
        {
            return userRepository.InsertGroupDetails(userGroupDetails);
        }

        public List<DtoSenderUserList> GetSenderUserList(string groupId)
        {
            return userRepository.GetSenderUserList(groupId);
        }

        public List<DtoGroupList> GetGroupList()
        {
            return userRepository.GetGroupList();
        }

        public List<DtoRecipientUserList> GetRecipientsUserList(string groupId)
        {
            return userRepository.GetRecipientsUserList(groupId);
        }

        public List<UsersInfoDto> GetUsersByNameForAllDesignations(string userName, string userId)
        {
            return userRepository.GetUsersByNameForAllDesignations(userName, userId);
        }

        public Task<DtoUserProfile> SaveUserProfile(DtoUserProfile userProfile)
        {
            return userRepository.SaveUserProfile(userProfile);
        }

        public int GetUserStatusbyPhoneNumber(string phoneNumber)
        {
            return userRepository.GetUserStatusbyPhoneNumber(phoneNumber);
        }

        public int SendMessage()
        {
            return userRepository.SendMessage();
        }

        public string CheckConflictonStatusUpdate(DtoUserStatus userStatus)
        {
            return userRepository.CheckConflictonStatusUpdate(userStatus);
        }

        public int InsertTransfer(List<TransferUser> transferUsers)
        {
            return userRepository.InsertTransfer(transferUsers);
        }

        public List<TransferHistory> GetTransferHistory(string userId)
        {
            return userRepository.GetTransferHistory(userId);
        }

        public int SaveUserPersonalInformation(DtoUserProfile userProfile)
        {
            return userRepository.SaveUserPersonalInformation(userProfile);
        }

        public List<AppUserName> ApproveUsers(ApproveUserStatusRequest userStatus)
        {
            return userRepository.ApproveUsers(userStatus);
        }

        public List<ConflictUsers> GetConflictUsers(string userId)
        {
            return userRepository.GetConflictUsers(userId);
        }

        public int ResetUserPassword(string userName)
        {
            return userRepository.ResetUserPassword(userName);
        }

        public int SaveRefreshToken(string userId, string refreshToken, DateTime refreshTokenExpiryTime)
        {
            return userRepository.SaveRefreshToken(userId, refreshToken, refreshTokenExpiryTime);
        }

        public TokenData GetToken(string userId)
        {
            return userRepository.GetToken(userId);
        }

        public string SaveNotificationToken(PushNotificationInfo notification)
        {
            return userRepository.SaveNotificationToken(notification);
        }

        public List<NotificationResponse> SendNotification(NotificationItem notify)
        {
            return this.userRepository.SendNotification(notify);
        }

        public string MarqueMesssage()
        {
            return this.userRepository.MarqueMesssage();
        }
    }
}
