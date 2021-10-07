﻿using PnP.Core.Services;
using System.Threading.Tasks;

namespace PnP.Core.Model.Security
{
    /// <summary>
    /// Public interface to define a SharePoint Group
    /// </summary>
    [ConcreteType(typeof(SharePointGroup))]
    public interface ISharePointGroup : IDataModel<ISharePointGroup>, IDataModelGet<ISharePointGroup>, IDataModelLoad<ISharePointGroup>, IDataModelUpdate, IDataModelDelete, ISharePointPrincipal, IQueryableDataModel
    {
        /// <summary>
        /// Allow members to edit the group members
        /// </summary>
        public bool AllowMembersEditMembership { get; set; }

        /// <summary>
        /// Allow requests to join or leave the group
        /// </summary>
        public bool AllowRequestToJoinLeave { get; set; }

        /// <summary>
        /// Automatically accept requests to join or leave the group
        /// </summary>
        public bool AutoAcceptRequestToJoinLeave { get; set; }

        /// <summary>
        /// Can the current user edit the group members
        /// </summary>
        public bool CanCurrentUserEditMembership { get; set; }

        /// <summary>
        /// Can the current user manage the group
        /// </summary>
        public bool CanCurrentUserManageGroup { get; set; }

        /// <summary>
        /// Can the current user view the group membership
        /// </summary>
        public bool CanCurrentUserViewMembership { get; set; }

        /// <summary>
        /// Group description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Only members can see other group members
        /// </summary>
        public bool OnlyAllowMembersViewMembership { get; set; }

        /// <summary>
        /// Group owner title
        /// </summary>
        public string OwnerTitle { get; set; }

        /// <summary>
        /// Email configuration for the group join or leave operations
        /// </summary>
        public bool RequestToJoinLeaveEmailSetting { get; set; }

        /// <summary>
        /// Members of this group
        /// </summary>
        public ISharePointUserCollection Users { get; }

        /// <summary>
        /// A special property used to add an asterisk to a $select statement
        /// </summary>
        public object All { get; }

        /// <summary>
        /// Adds a user to this group. Specify the full username, e.g. "i:0#.f|membership|user@domain.com"
        /// </summary>
        public void AddUser(string loginName);

        /// <summary>
        /// Adds a user to this group. Specify the full username, e.g. "i:0#.f|membership|user@domain.com"
        /// </summary>
        public Task AddUserAsync(string loginName);

        /// <summary>
        /// Adds a user to this group. Specify the full username, e.g. "i:0#.f|membership|user@domain.com"
        /// </summary>
        public void AddUserBatch(string loginName);

        /// <summary>
        /// Adds a user to this group. Specify the full username, e.g. "i:0#.f|membership|user@domain.com"
        /// </summary>
        public Task AddUserBatchAsync(string loginName);

        /// <summary>
        /// Adds a user to this group. Specify the full username, e.g. "i:0#.f|membership|user@domain.com"
        /// </summary>
        public void AddUserBatch(Batch batch, string loginName);

        /// <summary>
        /// Adds a user to this group. Specify the full username, e.g. "i:0#.f|membership|user@domain.com"
        /// </summary>
        public Task AddUserBatchAsync(Batch batch, string loginName);

        /// <summary>
        /// Removes a user given its id from a group.
        /// </summary>
        public void RemoveUser(int userId);

        /// <summary>
        /// Removes a user given its id from a group.
        /// </summary>
        public Task RemoveUserAsync(int userId);

        /// <summary>
        /// Removes a user given its id from a group.
        /// </summary>
        public void RemoveUserBatch(int userId);

        /// <summary>
        /// Removes a user given its id from a group.
        /// </summary>
        public Task RemoveUserBatchAsync(int userId);

        /// <summary>
        /// Removes a user given its id from a group.
        /// </summary>
        public void RemoveUserBatch(Batch batch, int userId);

        /// <summary>
        /// Removes a user given its id from a group.
        /// </summary>
        public Task RemoveUserBatchAsync(Batch batch, int userId);

        /// <summary>
        /// Retrieves the role definitions for this group
        /// </summary>
        public IRoleDefinitionCollection GetRoleDefinitions();

        /// <summary>
        /// Retrieves the role definitions for this group
        /// </summary>
        public Task<IRoleDefinitionCollection> GetRoleDefinitionsAsync();

        /// <summary>
        /// Adds role definitions for this group
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public bool AddRoleDefinitions(params string[] names);

        /// <summary>
        /// Adds role definitions for this group
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public Task<bool> AddRoleDefinitionsAsync(params string[] names);

        /// <summary>
        /// Removes role definitions for this group
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public bool RemoveRoleDefinitions(params string[] names);

        /// <summary>
        /// Removes role definitions for this group
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public Task<bool> RemoveRoleDefinitionsAsync(params string[] names);
    }
}
