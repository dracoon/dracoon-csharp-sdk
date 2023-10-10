namespace Dracoon.Sdk.Filter {
    /// <summary>
    ///     This class provides the filters for <see cref="Dracoon.Sdk.IShares.GetUploadShares(long?, long?, GetUploadSharesFilter, Sort.SharesSort)"/>.
    /// </summary>
    public class GetUploadSharesFilter : DracoonFilter {
        /// <summary>
        ///     Gets a new filter for the Name field of a upload share (<see cref="Dracoon.Sdk.Model.UploadShare"/>).
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.Filter.NameFilter"/>
        ///     </para>
        /// </summary>
        public static NameFilter Name => new NameFilter();

        /// <summary>
        ///     Gets a new filter for the CreatedBy field of a upload share (<see cref="Dracoon.Sdk.Model.UploadShare"/>).
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.Filter.UserIdFilter"/>
        ///     </para>
        /// </summary>
        public static UserIdFilter UserId => new UserIdFilter();

        /// <summary>
        ///     Gets a new filter for created by.
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.Filter.CreatedByFilter"/>
        ///     </para>
        /// </summary>
        public static CreatedByFilter CreatedBy => new CreatedByFilter();

        /// <summary>
        ///     Gets a new filter for the referenced node id.
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.Filter.NodeIdFilter"/>
        ///     </para>
        /// </summary>
        public static NodeIdFilter NodeId => new NodeIdFilter("targetId");

        /// <summary>
        ///     Gets a new filter for the access key.
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.Filter.AccessKeyFilter"/>
        ///     </para>
        /// </summary>
        public static AccessKeyFilter AccessKey => new AccessKeyFilter();

        /// <summary>
        ///     Adds a name filter to the get upload shares filter.
        /// </summary>
        /// <param name="nameFilter">The defined name filter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddNameFilter(DracoonFilterType<NameFilter> nameFilter) {
            CheckFilter(nameFilter, nameof(nameFilter));
            FiltersList.Add(nameFilter);
        }

        /// <summary>
        ///     Adds a user id filter to the get upload shares filter.
        /// </summary>
        /// <param name="userIdFilter">The defined user id filter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddUserIdFilter(DracoonFilterType<UserIdFilter> userIdFilter) {
            CheckFilter(userIdFilter, nameof(userIdFilter));
            FiltersList.Add(userIdFilter);
        }

        /// <summary>
        ///     Adds a created by filter to the get upload shares filter.
        /// </summary>
        /// <param name="createdByFilter">The defined created by filter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddCreatedByFilter(DracoonFilterType<CreatedByFilter> createdByFilter) {
            CheckFilter(createdByFilter, nameof(createdByFilter));
            FiltersList.Add(createdByFilter);
        }

        /// <summary>
        ///     Adds a node id filter to the get upload shares filter.
        /// </summary>
        /// <param name="nodeIdFilter">The defined node id filter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddNodeIdFilter(DracoonFilterType<NodeIdFilter> nodeIdFilter) {
            CheckFilter(nodeIdFilter, nameof(nodeIdFilter));
            FiltersList.Add(nodeIdFilter);
        }

        /// <summary>
        ///     Adds a access key filter to the get upload shares filter.
        /// </summary>
        /// <param name="accessKeyFilter">The defined access key filter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddAccessKeyFilter(DracoonFilterType<AccessKeyFilter> accessKeyFilter) {
            CheckFilter(accessKeyFilter, nameof(accessKeyFilter));
            FiltersList.Add(accessKeyFilter);
        }
    }
}