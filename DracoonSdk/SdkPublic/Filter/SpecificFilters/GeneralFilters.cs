using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.Validator;

namespace Dracoon.Sdk.Filter {
    #region NodeType-Filter

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/NodeTypeFilter/*'/>
    public class NodeTypeFilter : DracoonFilterType<NodeTypeFilter> {
        internal NodeTypeFilter() {
            FilterTypeString += "type";
        }
    }

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/NodeTypeFilterExtension/*'/>
    public static class NodeTypeFilterExtension {
        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="general"]/Or/*'/>
        public static NodeTypeFilter Or(this FilterParam<NodeTypeFilter, DracoonFilterType<NodeTypeFilter>> ef) {
            return ef.Parent;
        }

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="general"]/EqualTo/*'/>
        public static FilterParam<NodeTypeFilter, DracoonFilterType<NodeTypeFilter>> EqualTo(this NodeTypeFilter ef, NodeType value) {
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<NodeTypeFilter, DracoonFilterType<NodeTypeFilter>>(ef, ef);
        }
    }

    #endregion

    #region IsFavorite-Filter

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/IsFavoriteFilter/*'/>
    public class IsFavoriteFilter : DracoonFilterType<IsFavoriteFilter> {
        internal IsFavoriteFilter() {
            FilterTypeString += "isFavorite";
        }
    }

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/NodeIsFavoriteFilterExtension/*'/>
    public static class NodeIsFavoriteFilterExtension {
        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="general"]/EqualTo/*'/>
        public static FilterParam<IsFavoriteFilter, DracoonFilterType<IsFavoriteFilter>> EqualTo(this IsFavoriteFilter ef, bool value) {
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<IsFavoriteFilter, DracoonFilterType<IsFavoriteFilter>>(ef, ef);
        }
    }

    #endregion

    #region Name-Filter

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/NameFilter/*'/>
    public class NameFilter : DracoonFilterType<NameFilter> {
        internal NameFilter() {
            FilterTypeString += "name";
        }
    }

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/NodeNameFilterExtension/*'/>
    public static class NodeNameFilterExtension {
        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="general"]/Contains/*'/>
        public static FilterParam<NameFilter, DracoonFilterType<NameFilter>> Contains(this NameFilter ef, string value) {
            value.MustNotNullOrEmptyOrWhitespace(nameof(value));
            ef.AddOperatorAndValue(value, "cn", nameof(value));
            return new FilterParam<NameFilter, DracoonFilterType<NameFilter>>(ef, ef);
        }
    }

    #endregion

    #region IsEncrypted-Filter

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/NodeIsEncryptedFilter/*'/>
    public class NodeIsEncryptedFilter : DracoonFilterType<NodeIsEncryptedFilter> {
        internal NodeIsEncryptedFilter() {
            FilterTypeString += "encrypted";
        }
    }

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/NodeIsEncryptedFilterExtension/*'/>
    public static class NodeIsEncryptedFilterExtension {
        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="general"]/EqualTo/*'/>
        public static FilterParam<NodeIsEncryptedFilter, DracoonFilterType<NodeIsEncryptedFilter>>
            EqualTo(this NodeIsEncryptedFilter ef, bool value) {
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<NodeIsEncryptedFilter, DracoonFilterType<NodeIsEncryptedFilter>>(ef, ef);
        }
    }

    #endregion

    #region UserId-Filter

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/UserIdFilter/*'/>
    public class UserIdFilter : DracoonFilterType<UserIdFilter> {
        internal UserIdFilter() {
            FilterTypeString += "userId";
        }
    }

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/UserIdFilterExtension/*'/>
    public static class UserIdFilterExtension {
        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="general"]/EqualTo/*'/>
        public static FilterParam<UserIdFilter, DracoonFilterType<UserIdFilter>> EqualTo(this UserIdFilter ef, long value) {
            value.MustPositive(nameof(value));
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<UserIdFilter, DracoonFilterType<UserIdFilter>>(ef, ef);
        }
    }

    #endregion

    #region UpdatedBy-Filter

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/UpdatedByFilter/*'/>
    public class UpdatedByFilter : DracoonFilterType<UpdatedByFilter> {
        internal UpdatedByFilter() {
            FilterTypeString += "updatedBy";
        }
    }

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/UpdatedByFilterExtension/*'/>
    public static class UpdatedByFilterExtension {
        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="general"]/EqualTo/*'/>
        public static FilterParam<UpdatedByFilter, DracoonFilterType<UpdatedByFilter>> EqualTo(this UpdatedByFilter ef, string value) {
            value.MustNotNullOrEmptyOrWhitespace(nameof(value));
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<UpdatedByFilter, DracoonFilterType<UpdatedByFilter>>(ef, ef);
        }

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="general"]/Contains/*'/>
        public static FilterParam<UpdatedByFilter, DracoonFilterType<UpdatedByFilter>> Contains(this UpdatedByFilter ef, string value) {
            value.MustNotNullOrEmptyOrWhitespace(nameof(value));
            ef.AddOperatorAndValue(value, "cn", nameof(value));
            return new FilterParam<UpdatedByFilter, DracoonFilterType<UpdatedByFilter>>(ef, ef);
        }
    }

    #endregion

    #region UpdatedById-Filter

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/UpdatedByIdFilter/*'/>
    public class UpdatedByIdFilter : DracoonFilterType<UpdatedByIdFilter> {
        internal UpdatedByIdFilter() {
            FilterTypeString += "updatedById";
        }
    }

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/UpdatedByIdFilterExtension/*'/>
    public static class UpdatedByIdFilterExtension {
        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="general"]/EqualTo/*'/>
        public static FilterParam<UpdatedByIdFilter, DracoonFilterType<UpdatedByIdFilter>> EqualTo(this UpdatedByIdFilter ef, long value) {
            value.MustPositive(nameof(value));
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<UpdatedByIdFilter, DracoonFilterType<UpdatedByIdFilter>>(ef, ef);
        }
    }

    #endregion

    #region ParentPath-Filter

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/ParentPathFilter/*'/>
    public class ParentPathFilter : DracoonFilterType<ParentPathFilter> {
        internal ParentPathFilter() {
            FilterTypeString += "parentPath";
        }
    }

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/ParentPathFilterExtension/*'/>
    public static class ParentPathFilterExtension {
        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="general"]/EqualTo/*'/>
        public static FilterParam<ParentPathFilter, DracoonFilterType<ParentPathFilter>> EqualTo(this ParentPathFilter ef, string value) {
            value.MustNotNullOrEmptyOrWhitespace(nameof(value));
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<ParentPathFilter, DracoonFilterType<ParentPathFilter>>(ef, ef);
        }

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="general"]/Contains/*'/>
        public static FilterParam<ParentPathFilter, DracoonFilterType<ParentPathFilter>> Contains(this ParentPathFilter ef, string value) {
            value.MustNotNullOrEmptyOrWhitespace(nameof(value));
            ef.AddOperatorAndValue(value, "cn", nameof(value));
            return new FilterParam<ParentPathFilter, DracoonFilterType<ParentPathFilter>>(ef, ef);
        }
    }

    #endregion

    #region FileType-Filter

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/FileTypeFilter/*'/>
    public class FileTypeFilter : DracoonFilterType<FileTypeFilter> {
        internal FileTypeFilter() {
            FilterTypeString += "fileType";
        }
    }

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/FileTypeFilterExtension/*'/>
    public static class FileTypeFilterExtension {
        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="general"]/EqualTo/*'/>
        public static FilterParam<FileTypeFilter, DracoonFilterType<FileTypeFilter>> EqualTo(this FileTypeFilter ef, string value) {
            value.MustNotNullOrEmptyOrWhitespace(nameof(value));
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<FileTypeFilter, DracoonFilterType<FileTypeFilter>>(ef, ef);
        }

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="general"]/Contains/*'/>
        public static FilterParam<FileTypeFilter, DracoonFilterType<FileTypeFilter>> Contains(this FileTypeFilter ef, string value) {
            value.MustNotNullOrEmptyOrWhitespace(nameof(value));
            ef.AddOperatorAndValue(value, "cn", nameof(value));
            return new FilterParam<FileTypeFilter, DracoonFilterType<FileTypeFilter>>(ef, ef);
        }
    }

    #endregion

    #region Classification-Filter

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/ClassificationFilter/*'/>
    public class ClassificationFilter : DracoonFilterType<ClassificationFilter> {
        internal ClassificationFilter() {
            FilterTypeString += "classification";
        }
    }

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/ClassificationFilterExtension/*'/>
    public static class ClassificationFilterExtension {
        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="general"]/EqualTo/*'/>
        public static FilterParam<ClassificationFilter, DracoonFilterType<ClassificationFilter>> EqualTo(this ClassificationFilter ef,
            Classification value) {
            ef.AddOperatorAndValue((int) value, "eq", nameof(value));
            return new FilterParam<ClassificationFilter, DracoonFilterType<ClassificationFilter>>(ef, ef);
        }
    }

    #endregion

    #region CreatedBy-Filter

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/CreatedByFilter/*'/>
    public class CreatedByFilter : DracoonFilterType<CreatedByFilter> {
        internal CreatedByFilter() {
            FilterTypeString += "createdBy";
        }
    }

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/CreatedByFilterExtension/*'/>
    public static class CreatedByFilterExtension {
        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="general"]/Contains/*'/>
        public static FilterParam<CreatedByFilter, DracoonFilterType<CreatedByFilter>> Contains(this CreatedByFilter ef, string value) {
            value.MustNotNullOrEmptyOrWhitespace(nameof(value));
            ef.AddOperatorAndValue(value, "cn", nameof(value));
            return new FilterParam<CreatedByFilter, DracoonFilterType<CreatedByFilter>>(ef, ef);
        }
    }

    #endregion

    #region CreatedById-Filter

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/CreatedByIdFilter/*'/>
    public class CreatedByIdFilter : DracoonFilterType<CreatedByIdFilter> {
        internal CreatedByIdFilter() {
            FilterTypeString += "createdById";
        }
    }

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/CreatedByIdFilterExtension/*'/>
    public static class CreatedByIdFilterExtension {
        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="general"]/Contains/*'/>
        public static FilterParam<CreatedByIdFilter, DracoonFilterType<CreatedByIdFilter>> EqualTo(this CreatedByIdFilter ef, long value) {
            value.MustPositive(nameof(value));
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<CreatedByIdFilter, DracoonFilterType<CreatedByIdFilter>>(ef, ef);
        }
    }

    #endregion

    #region NodeId_TargetId-Filter

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/TargetIdFilter/*'/>
    public class NodeIdFilter : DracoonFilterType<NodeIdFilter> {
        internal NodeIdFilter(string filterName) {
            FilterTypeString += filterName;
        }
    }

    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="generalFilters"]/TargetIdFilterExtension/*'/>
    public static class TargetIdFilterExtension {
        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="general"]/EqualTo/*'/>
        public static FilterParam<NodeIdFilter, DracoonFilterType<NodeIdFilter>> EqualTo(this NodeIdFilter ef, long value) {
            value.MustPositive(nameof(value));
            ef.AddOperatorAndValue(value, "eq", nameof(value));
            return new FilterParam<NodeIdFilter, DracoonFilterType<NodeIdFilter>>(ef, ef);
        }
    }

    #endregion
}