using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.Validator;
using System;

namespace Dracoon.Sdk.Filter {
    #region NodeType-Filter

    /// <summary>
    ///     Filter for the field 'Type' of a node. See also <seealso cref="Dracoon.Sdk.Model.Node"/>
    /// </summary>
    public class NodeTypeFilter : DracoonFilterType<NodeTypeFilter> {
        internal NodeTypeFilter() {
            FilterName = "type";
            FilterTypeString += FilterName;
        }
    }

    /// <summary>
    ///     Extends the functions for <see cref="Dracoon.Sdk.Filter.NodeTypeFilter"/>.
    /// </summary>
    public static class NodeTypeFilterExtension {
        /// <summary>
        ///     Adds the possibility to define another value for the given filter which should be fulfilled.
        /// </summary>
        /// <param name="ef">The class for which the extension is provided.</param>
        /// <returns>The class for which the extension is provided.</returns>
        public static NodeTypeFilter Or(this FilterParam<NodeTypeFilter, DracoonFilterType<NodeTypeFilter>> ef) {
            return ef.Parent;
        }

        /// <summary>
        ///     Adds a "equal" filter for the extended filter class.
        /// </summary>
        /// <param name="ef">The class for which the extension is provided.</param>
        /// <param name="value">The value which should be equal in the result.</param>
        /// <returns>The filter parameter which can possibly add a additional condition. See also <seealso cref="Dracoon.Sdk.Filter.FilterParam{T, V}"/></returns>
        public static FilterParam<NodeTypeFilter, DracoonFilterType<NodeTypeFilter>> EqualTo(this NodeTypeFilter ef, NodeType value) {
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<NodeTypeFilter, DracoonFilterType<NodeTypeFilter>>(ef, ef);
        }
    }

    #endregion

    #region IsFavorite-Filter

    /// <summary>
    ///     Filter for the field 'IsFavorite' of a node. See also <seealso cref="Dracoon.Sdk.Model.Node"/>
    /// </summary>
    public class IsFavoriteFilter : DracoonFilterType<IsFavoriteFilter> {
        internal IsFavoriteFilter() {
            FilterName = "isFavorite";
            FilterTypeString += FilterName;
        }
    }

    /// <summary>
    ///     Extends the functions for <see cref="Dracoon.Sdk.Filter.IsFavoriteFilter"/>.
    /// </summary>
    public static class NodeIsFavoriteFilterExtension {
        /// <summary>
        ///     Adds a "equal" filter for the extended filter class.
        /// </summary>
        /// <param name="ef">The class for which the extension is provided.</param>
        /// <param name="value">The value which should be equal in the result.</param>
        /// <returns>The filter parameter which can possibly add a additional condition. See also <seealso cref="Dracoon.Sdk.Filter.FilterParam{T, V}"/></returns>
        public static FilterParam<IsFavoriteFilter, DracoonFilterType<IsFavoriteFilter>> EqualTo(this IsFavoriteFilter ef, bool value) {
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<IsFavoriteFilter, DracoonFilterType<IsFavoriteFilter>>(ef, ef);
        }
    }

    #endregion

    #region Name-Filter

    /// <summary>
    ///     Filter for the field 'Name' of a node. See also <seealso cref="Dracoon.Sdk.Model.Node"/>
    /// </summary>
    public class NameFilter : DracoonFilterType<NameFilter> {
        internal NameFilter() {
            FilterName = "name";
            FilterTypeString += FilterName;
        }
    }

    /// <summary>
    ///     Extends the functions for <see cref="Dracoon.Sdk.Filter.NameFilter"/>.
    /// </summary>
    public static class NodeNameFilterExtension {
        /// <summary>
        ///     Adds a "contains" filter for the extended filter class.
        /// </summary>
        /// <param name="ef">The class for which the extension is provided.</param>
        /// <param name="value">The value which should be contained the result for the specified field.</param>
        /// <returns>The filter parameter which can possibly add a additional condition. See also <seealso cref="Dracoon.Sdk.Filter.FilterParam{T, V}"/></returns>
        public static FilterParam<NameFilter, DracoonFilterType<NameFilter>> Contains(this NameFilter ef, string value) {
            value.MustNotNullOrEmptyOrWhitespace(nameof(value));
            ef.AddOperatorAndValue(value, "cn", nameof(value));
            return new FilterParam<NameFilter, DracoonFilterType<NameFilter>>(ef, ef);
        }
    }

    #endregion

    #region IsEncrypted-Filter

    /// <summary>
    ///     Filter for the field 'IsEncrypted' of a node. See also <seealso cref="Dracoon.Sdk.Model.Node"/>
    /// </summary>
    public class NodeIsEncryptedFilter : DracoonFilterType<NodeIsEncryptedFilter> {
        internal NodeIsEncryptedFilter() {
            FilterName = "encrypted";
            FilterTypeString += FilterName;
        }
    }

    /// <summary>
    ///     Extends the functions for <see cref="Dracoon.Sdk.Filter.NodeIsEncryptedFilter"/>.
    /// </summary>
    public static class NodeIsEncryptedFilterExtension {
        /// <summary>
        ///     Adds a "equal" filter for the extended filter class.
        /// </summary>
        /// <param name="ef">The class for which the extension is provided.</param>
        /// <param name="value">The value which should be equal in the result.</param>
        /// <returns>The filter parameter which can possibly add a additional condition. See also <seealso cref="Dracoon.Sdk.Filter.FilterParam{T, V}"/></returns>
        public static FilterParam<NodeIsEncryptedFilter, DracoonFilterType<NodeIsEncryptedFilter>> EqualTo(this NodeIsEncryptedFilter ef, bool value) {
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<NodeIsEncryptedFilter, DracoonFilterType<NodeIsEncryptedFilter>>(ef, ef);
        }
    }

    #endregion

    #region UserId-Filter

    /// <summary>
    ///     Filter for a specific user.
    /// </summary>
    public class UserIdFilter : DracoonFilterType<UserIdFilter> {
        internal UserIdFilter() {
            FilterName = "userId";
            FilterTypeString += FilterName;
        }
    }

    /// <summary>
    ///     Extends the functions for <see cref="Dracoon.Sdk.Filter.UserIdFilter"/>.
    /// </summary>
    public static class UserIdFilterExtension {
        /// <summary>
        ///     Adds a "equal" filter for the extended filter class.
        /// </summary>
        /// <param name="ef">The class for which the extension is provided.</param>
        /// <param name="value">The value which should be equal in the result.</param>
        /// <returns>The filter parameter which can possibly add a additional condition. See also <seealso cref="Dracoon.Sdk.Filter.FilterParam{T, V}"/></returns>
        public static FilterParam<UserIdFilter, DracoonFilterType<UserIdFilter>> EqualTo(this UserIdFilter ef, long value) {
            value.MustPositive(nameof(value));
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<UserIdFilter, DracoonFilterType<UserIdFilter>>(ef, ef);
        }
    }

    #endregion

    #region UpdatedBy-Filter

    /// <summary>
    ///     Filter for a specific user which updated a e.g. node.
    /// </summary>
    public class UpdatedByFilter : DracoonFilterType<UpdatedByFilter> {
        internal UpdatedByFilter() {
            FilterName = "updatedBy";
            FilterTypeString += FilterName;
        }
    }

    /// <summary>
    ///     Extends the functions for <see cref="Dracoon.Sdk.Filter.UpdatedByFilter"/>.
    /// </summary>
    public static class UpdatedByFilterExtension {
        /// <summary>
        ///     Adds a "equal" filter for the extended filter class.
        /// </summary>
        /// <param name="ef">The class for which the extension is provided.</param>
        /// <param name="value">The value which should be equal in the result.</param>
        /// <returns>The filter parameter which can possibly add a additional condition. See also <seealso cref="Dracoon.Sdk.Filter.FilterParam{T, V}"/></returns>
        public static FilterParam<UpdatedByFilter, DracoonFilterType<UpdatedByFilter>> EqualTo(this UpdatedByFilter ef, string value) {
            value.MustNotNullOrEmptyOrWhitespace(nameof(value));
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<UpdatedByFilter, DracoonFilterType<UpdatedByFilter>>(ef, ef);
        }

        /// <summary>
        ///     Adds a "contains" filter for the extended filter class.
        /// </summary>
        /// <param name="ef">The class for which the extension is provided.</param>
        /// <param name="value">The value which should be contained the result for the specified field.</param>
        /// <returns>The filter parameter which can possibly add a additional condition. See also <seealso cref="Dracoon.Sdk.Filter.FilterParam{T, V}"/></returns>
        public static FilterParam<UpdatedByFilter, DracoonFilterType<UpdatedByFilter>> Contains(this UpdatedByFilter ef, string value) {
            value.MustNotNullOrEmptyOrWhitespace(nameof(value));
            ef.AddOperatorAndValue(value, "cn", nameof(value));
            return new FilterParam<UpdatedByFilter, DracoonFilterType<UpdatedByFilter>>(ef, ef);
        }
    }

    #endregion

    #region UpdatedById-Filter

    /// <summary>
    ///     Filter for a specific user id which updated a e.g. node.
    /// </summary>
    public class UpdatedByIdFilter : DracoonFilterType<UpdatedByIdFilter> {
        internal UpdatedByIdFilter() {
            FilterName = "updatedById";
            FilterTypeString += FilterName;
        }
    }

    /// <summary>
    ///     Extends the functions for <see cref="Dracoon.Sdk.Filter.UpdatedByIdFilter"/>.
    /// </summary>
    public static class UpdatedByIdFilterExtension {
        /// <summary>
        ///     Adds a "equal" filter for the extended filter class.
        /// </summary>
        /// <param name="ef">The class for which the extension is provided.</param>
        /// <param name="value">The value which should be equal in the result.</param>
        /// <returns>The filter parameter which can possibly add a additional condition. See also <seealso cref="Dracoon.Sdk.Filter.FilterParam{T, V}"/></returns>
        public static FilterParam<UpdatedByIdFilter, DracoonFilterType<UpdatedByIdFilter>> EqualTo(this UpdatedByIdFilter ef, long value) {
            value.MustPositive(nameof(value));
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<UpdatedByIdFilter, DracoonFilterType<UpdatedByIdFilter>>(ef, ef);
        }
    }

    #endregion

    #region ParentPath-Filter

    /// <summary>
    ///     Filter to get only results where the parent path field matches the requested.
    /// </summary>
    public class ParentPathFilter : DracoonFilterType<ParentPathFilter> {
        internal ParentPathFilter() {
            FilterName = "parentPath";
            FilterTypeString += FilterName;
        }
    }

    /// <summary>
    ///     Extends the functions for <see cref="Dracoon.Sdk.Filter.ParentPathFilter"/>.
    /// </summary>
    public static class ParentPathFilterExtension {
        /// <summary>
        ///     Adds a "equal" filter for the extended filter class.
        /// </summary>
        /// <param name="ef">The class for which the extension is provided.</param>
        /// <param name="value">The value which should be equal in the result.</param>
        /// <returns>The filter parameter which can possibly add a additional condition. See also <seealso cref="Dracoon.Sdk.Filter.FilterParam{T, V}"/></returns>
        public static FilterParam<ParentPathFilter, DracoonFilterType<ParentPathFilter>> EqualTo(this ParentPathFilter ef, string value) {
            value.MustNotNullOrEmptyOrWhitespace(nameof(value));
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<ParentPathFilter, DracoonFilterType<ParentPathFilter>>(ef, ef);
        }

        /// <summary>
        ///     Adds a "contains" filter for the extended filter class.
        /// </summary>
        /// <param name="ef">The class for which the extension is provided.</param>
        /// <param name="value">The value which should be contained the result for the specified field.</param>
        /// <returns>The filter parameter which can possibly add a additional condition. See also <seealso cref="Dracoon.Sdk.Filter.FilterParam{T, V}"/></returns>
        public static FilterParam<ParentPathFilter, DracoonFilterType<ParentPathFilter>> Contains(this ParentPathFilter ef, string value) {
            value.MustNotNullOrEmptyOrWhitespace(nameof(value));
            ef.AddOperatorAndValue(value, "cn", nameof(value));
            return new FilterParam<ParentPathFilter, DracoonFilterType<ParentPathFilter>>(ef, ef);
        }
    }

    #endregion

    #region FileType-Filter

    /// <summary>
    ///     Filter to get only results where the file type has the specified extension. E.g. 'ico', 'txt', ...
    /// </summary>
    public class FileTypeFilter : DracoonFilterType<FileTypeFilter> {
        internal FileTypeFilter() {
            FilterName = "fileType";
            FilterTypeString += FilterName;
        }
    }

    /// <summary>
    ///     Extends the functions for <see cref="Dracoon.Sdk.Filter.FileTypeFilter"/>.
    /// </summary>
    public static class FileTypeFilterExtension {
        /// <summary>
        ///     Adds a "equal" filter for the extended filter class.
        /// </summary>
        /// <param name="ef">The class for which the extension is provided.</param>
        /// <param name="value">The value which should be equal in the result.</param>
        /// <returns>The filter parameter which can possibly add a additional condition. See also <seealso cref="Dracoon.Sdk.Filter.FilterParam{T, V}"/></returns>
        public static FilterParam<FileTypeFilter, DracoonFilterType<FileTypeFilter>> EqualTo(this FileTypeFilter ef, string value) {
            value.MustNotNullOrEmptyOrWhitespace(nameof(value));
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<FileTypeFilter, DracoonFilterType<FileTypeFilter>>(ef, ef);
        }

        /// <summary>
        ///     Adds a "contains" filter for the extended filter class.
        /// </summary>
        /// <param name="ef">The class for which the extension is provided.</param>
        /// <param name="value">The value which should be contained the result for the specified field.</param>
        /// <returns>The filter parameter which can possibly add a additional condition. See also <seealso cref="Dracoon.Sdk.Filter.FilterParam{T, V}"/></returns>
        public static FilterParam<FileTypeFilter, DracoonFilterType<FileTypeFilter>> Contains(this FileTypeFilter ef, string value) {
            value.MustNotNullOrEmptyOrWhitespace(nameof(value));
            ef.AddOperatorAndValue(value, "cn", nameof(value));
            return new FilterParam<FileTypeFilter, DracoonFilterType<FileTypeFilter>>(ef, ef);
        }
    }

    #endregion

    #region Classification-Filter

    /// <summary>
    ///     Filter to get only results where the classification is of the specified type.
    /// </summary>
    public class ClassificationFilter : DracoonFilterType<ClassificationFilter> {
        internal ClassificationFilter() {
            FilterName = "classification";
            FilterTypeString += FilterName;
        }
    }

    /// <summary>
    ///     Extends the functions for <see cref="Dracoon.Sdk.Filter.ClassificationFilter"/>.
    /// </summary>
    public static class ClassificationFilterExtension {
        /// <summary>
        ///     Adds a "equal" filter for the extended filter class.
        /// </summary>
        /// <param name="ef">The class for which the extension is provided.</param>
        /// <param name="value">The value which should be equal in the result.</param>
        /// <returns>The filter parameter which can possibly add a additional condition. See also <seealso cref="Dracoon.Sdk.Filter.FilterParam{T, V}"/></returns>
        public static FilterParam<ClassificationFilter, DracoonFilterType<ClassificationFilter>> EqualTo(this ClassificationFilter ef,
            Classification value) {
            ef.AddOperatorAndValue((int)value, "eq", nameof(value));
            return new FilterParam<ClassificationFilter, DracoonFilterType<ClassificationFilter>>(ef, ef);
        }
    }

    #endregion

    #region CreatedBy-Filter

    /// <summary>
    ///     Filter to get only results where the creator (firstname, lastname, login) contains the specified value.
    /// </summary>
    public class CreatedByFilter : DracoonFilterType<CreatedByFilter> {
        internal CreatedByFilter() {
            FilterName = "createdBy";
            FilterTypeString += FilterName;
        }
    }

    /// <summary>
    ///     Extends the functions for <see cref="Dracoon.Sdk.Filter.CreatedByFilter"/>.
    /// </summary>
    public static class CreatedByFilterExtension {
        /// <summary>
        ///     Adds a "contains" filter for the extended filter class.
        /// </summary>
        /// <param name="ef">The class for which the extension is provided.</param>
        /// <param name="value">The value which should be contained the result for the specified field.</param>
        /// <returns>The filter parameter which can possibly add a additional condition. See also <seealso cref="Dracoon.Sdk.Filter.FilterParam{T, V}"/></returns>
        public static FilterParam<CreatedByFilter, DracoonFilterType<CreatedByFilter>> Contains(this CreatedByFilter ef, string value) {
            value.MustNotNullOrEmptyOrWhitespace(nameof(value));
            ef.AddOperatorAndValue(value, "cn", nameof(value));
            return new FilterParam<CreatedByFilter, DracoonFilterType<CreatedByFilter>>(ef, ef);
        }
    }

    #endregion

    #region CreatedById-Filter

    /// <summary>
    ///     Filter to get only results where the creator id equals the specified value.
    /// </summary>
    public class CreatedByIdFilter : DracoonFilterType<CreatedByIdFilter> {
        internal CreatedByIdFilter() {
            FilterName = "createdById";
            FilterTypeString += FilterName;
        }
    }

    /// <summary>
    ///     Extends the functions for <see cref="Dracoon.Sdk.Filter.CreatedByIdFilter"/>.
    /// </summary>
    public static class CreatedByIdFilterExtension {
        /// <summary>
        ///     Adds a "contains" filter for the extended filter class.
        /// </summary>
        /// <param name="ef">The class for which the extension is provided.</param>
        /// <param name="value">The value which should be contained the result for the specified field.</param>
        /// <returns>The filter parameter which can possibly add a additional condition. See also <seealso cref="Dracoon.Sdk.Filter.FilterParam{T, V}"/></returns>
        public static FilterParam<CreatedByIdFilter, DracoonFilterType<CreatedByIdFilter>> EqualTo(this CreatedByIdFilter ef, long value) {
            value.MustPositive(nameof(value));
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<CreatedByIdFilter, DracoonFilterType<CreatedByIdFilter>>(ef, ef);
        }
    }

    #endregion

    #region NodeId_TargetId-Filter

    /// <summary>
    ///     Filter to get only results where the referenced node is the specified id.
    /// </summary>
    public class NodeIdFilter : DracoonFilterType<NodeIdFilter> {
        internal NodeIdFilter(string filterName) {
            FilterName = filterName;
            FilterTypeString += FilterName;
        }
    }

    /// <summary>
    ///     Extends the functions for <see cref="Dracoon.Sdk.Filter.NodeIdFilter"/>.
    /// </summary>
    public static class TargetIdFilterExtension {
        /// <summary>
        ///     Adds a "equal" filter for the extended filter class.
        /// </summary>
        /// <param name="ef">The class for which the extension is provided.</param>
        /// <param name="value">The value which should be equal in the result.</param>
        /// <returns>The filter parameter which can possibly add a additional condition. See also <seealso cref="Dracoon.Sdk.Filter.FilterParam{T, V}"/></returns>
        public static FilterParam<NodeIdFilter, DracoonFilterType<NodeIdFilter>> EqualTo(this NodeIdFilter ef, long value) {
            value.MustPositive(nameof(value));
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<NodeIdFilter, DracoonFilterType<NodeIdFilter>>(ef, ef);
        }
    }

    #endregion

    #region AccessKey-Filter

    /// <summary>
    ///     Filter to get only results where the given accesskey is contained.
    /// </summary>
    public class AccessKeyFilter : DracoonFilterType<AccessKeyFilter> {
        internal AccessKeyFilter() {
            FilterName = "accessKey";
            FilterTypeString += FilterName;
        }
    }

    /// <summary>
    ///     Extends the functions for <see cref="Dracoon.Sdk.Filter.AccessKeyFilter"/>
    /// </summary>
    public static class AccessKeyFilterExtension {
        /// <summary>
        ///     Adds a "contains" filter for the extended filter class.
        /// </summary>
        /// <param name="ef">The class for which the extension is provided.</param>
        /// <param name="value">The value which should be contained the result for the specified field.</param>
        /// <returns>The filter parameter which can possibly add a additional condition. See also <seealso cref="Dracoon.Sdk.Filter.FilterParam{T, V}"/></returns>
        public static FilterParam<AccessKeyFilter, DracoonFilterType<AccessKeyFilter>> Contains(this AccessKeyFilter ef, string value) {
            value.MustNotNullOrEmptyOrWhitespace(nameof(value));
            ef.AddOperatorAndValue(value, "cn", nameof(value));
            return new FilterParam<AccessKeyFilter, DracoonFilterType<AccessKeyFilter>>(ef, ef);
        }
    }

    #endregion

    #region timestampCreation

    /// <summary>
    ///     Filter to restrict the period of time.
    /// </summary>
    public class TimestampFilter : DracoonFilterType<TimestampFilter> {
        internal TimestampFilter(string filterName) {
            FilterName = filterName;
            FilterTypeString += FilterName;
        }
    }

    /// <summary>
    ///     Extends the functions for <see cref="Dracoon.Sdk.Filter.TimestampFilter"/>.
    /// </summary>
    public static class TimestampFilterExtension {
        /// <summary>
        ///     Adds a "greater or equal to" filter for the extended filter class.
        /// </summary>
        /// <param name="ef">The class for wich the extension is provided.</param>
        /// <param name="value">The value which should be greater or equal to in the result.</param>
        /// <returns>The filter parameter which can possibly add a additional condition. See also <seealso cref="Dracoon.Sdk.Filter.FilterParam{T, V}"/></returns>
        public static FilterParam<TimestampFilter, DracoonFilterType<TimestampFilter>> GreaterEqualTo(this TimestampFilter ef, DateTime value) {
            value.MustNotNull(nameof(value));
            ef.AddOperatorAndValue(value, "ge", nameof(value));
            return new FilterParam<TimestampFilter, DracoonFilterType<TimestampFilter>>(ef, ef);
        }

        /// <summary>
        ///     Adds a "less or equal to" filter for the extended filter class.
        /// </summary>
        /// <param name="ef">The class for wich the extension is provided.</param>
        /// <param name="value">The value which should be less or equal to in the result.</param>
        /// <returns>The filter parameter which can possibly add a additional condition. See also <seealso cref="Dracoon.Sdk.Filter.FilterParam{T, V}"/></returns>
        public static FilterParam<TimestampFilter, DracoonFilterType<TimestampFilter>> LessEqualTo(this TimestampFilter ef, DateTime value) {
            value.MustNotNull(nameof(value));
            ef.AddOperatorAndValue(value, "le", nameof(value));
            return new FilterParam<TimestampFilter, DracoonFilterType<TimestampFilter>>(ef, ef);
        }

        /// <summary>
        ///     Adds a "and" operator to concat multiple filters.
        /// </summary>
        /// <param name="ef">The class for wich the extension is provided.</param>
        /// <returns>The extended class itself with the possibility to add a additional filter.</returns>
        public static TimestampFilter And(this FilterParam<TimestampFilter, DracoonFilterType<TimestampFilter>> ef) {
            ef.Parent.AddAnd();
            return ef.Parent;
        }
    }

    #endregion
}