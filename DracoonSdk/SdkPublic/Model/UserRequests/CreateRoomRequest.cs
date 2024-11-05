using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Request to create a new room.
    /// </summary>
    public class CreateRoomRequest {

        /// <summary>
        ///     The parent node id for this ne created room.
        ///     <para>
        ///         Nullable. If not set, the new room will be a top level room.
        ///     </para>
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        ///     The name of the new room.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     The quota of the new room.
        /// </summary>
        public long? Quota { get; set; }

        /// <summary>
        ///     The notes of the new room.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        ///     The retention period for deleted nodes in days.
        /// </summary>
        public int? RecycleBinRetentionPeriod { get; set; }

        /// <summary>
        ///     If set to <c>true</c> the permissions from the parent room will be set.
        /// </summary>
        public bool? HasInheritPermissions { get; set; }

        /// <summary>
        ///     The list of user ids which have admin permissions on the new room.
        ///     <para>
        ///         Must contain at least one user id if no<see cref="CreateRoomRequest.AdminGroupIds"/> is set.
        ///     </para>
        /// </summary>
        public List<long> AdminUserIds { get; set; }

        /// <summary>
        ///     The list of group ids which have admin permissions on the new room.
        ///     <para>
        ///         Must contain at least one group id if no<see cref="CreateRoomRequest.AdminUserIds"/> is set.
        ///     </para>
        /// </summary>
        public List<long> AdminGroupIds { get; set; }

        /// <summary>
        ///     Behaviour when new users are added to the group. See also <seealso cref="Dracoon.Sdk.Model.GroupMemberAcceptance"/>.
        ///     <para>
        ///         Only relevant if at least one group id in <see cref = "CreateRoomRequest.AdminUserIds" /> is set.
        ///     </para>
        /// </summary>
        public GroupMemberAcceptance NewGroupMemberAcceptance { get; set; }

        /// <summary>
        ///     The classification for this node.
        ///     <para>
        ///         Nullable. If not set the parent room classification (or default if not available which is internal) is used.
        ///     </para>
        /// </summary>
        public Classification? Classification { get; set; }

        /// <summary>
        ///     The external creation time of this node.
        ///     <para>
        ///         Nullable. If not set, the default is the current server time in UTC.
        ///     </para>
        /// </summary>
        public DateTime? CreationTime { get; set; }

        /// <summary>
        ///     The content modification time of this node.
        ///     <para>
        ///         Nullable. If not set, the default is the current server time in UTC.
        ///     </para>
        /// </summary>
        public DateTime? ModificationTime { get; set; }

        /// <summary>
        ///     Indicates if the activities log is enabled
        ///     <para>
        ///         Default = true
        ///     </para>
        /// </summary>
        public bool HasActivitiesLog { get; set; }

        /// <summary>
        ///     Constructs a new create room request.
        /// </summary>
        /// <param name="name"><see cref="Name"/></param>
        /// <param name="parentId"><see cref="ParentId"/></param>
        /// <param name="newGroupMemberAcceptance"><see cref="NewGroupMemberAcceptance"/></param>
        /// <param name="hasActivitiesLog"><see cref="HasActivitiesLog"/></param>
        /// <param name="quota"><see cref="Quota"/></param>
        /// <param name="notes"><see cref="Notes"/></param>
        /// <param name="recycleBinRetentionPeriod"><see cref="RecycleBinRetentionPeriod"/></param>
        /// <param name="hasInheritPermissions"><see cref="HasInheritPermissions"/></param>
        /// <param name="adminUserIds"><see cref="AdminUserIds"/></param>
        /// <param name="adminGroupIds"><see cref="AdminGroupIds"/></param>
        /// <param name="classification"><see cref="Classification"/></param>
        /// <param name="creationTime"><see cref="CreationTime"/></param>
        /// <param name="modificationTime"><see cref="ModificationTime"/></param>
        public CreateRoomRequest(string name, long? parentId = null, GroupMemberAcceptance newGroupMemberAcceptance = GroupMemberAcceptance.AutoAllow, bool hasActivitiesLog = true,
            long? quota = null, string notes = null, int? recycleBinRetentionPeriod = null, bool? hasInheritPermissions = null,
            List<long> adminUserIds = null, List<long> adminGroupIds = null, Classification? classification = null, DateTime? creationTime = null, DateTime? modificationTime = null) {
            Name = name;
            ParentId = parentId;
            Quota = quota;
            Notes = notes;
            RecycleBinRetentionPeriod = recycleBinRetentionPeriod;
            HasInheritPermissions = hasInheritPermissions;
            AdminUserIds = adminUserIds;
            AdminGroupIds = adminGroupIds;
            NewGroupMemberAcceptance = newGroupMemberAcceptance;
            Classification = classification;
            CreationTime = creationTime;
            ModificationTime = modificationTime;
            HasActivitiesLog = hasActivitiesLog;
        }
    }
}