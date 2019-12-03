using Dracoon.Sdk.Model;
using System;

namespace Dracoon.Sdk.SdkInternal.Util {
    internal static class EnumConverter {
        public static Func<string, NodeType> ConvertValueToNodeTypeEnum = value => {
            switch (value) {
                case "room":
                    return NodeType.Room;
                case "folder":
                    return NodeType.Folder;
                default:
                    return NodeType.File;
            }
        };

        public static Func<NodeType, string> ConvertNodeTypeEnumToValue = typeEnum => {
            switch (typeEnum) {
                case NodeType.Room:
                    return "room";
                case NodeType.Folder:
                    return "folder";
                default:
                    return "file";
            }
        };

        public static Classification? ConvertValueToClassificationEnum(int? classificationValue) {
            switch (classificationValue) {
                case 1:
                    return Classification.Public;
                case 2:
                    return Classification.Internal;
                case 3:
                    return Classification.Confidential;
                case 4:
                    return Classification.StrictlyConfidential;
                default:
                    return null;
            }
        }

        public static int? ConvertClassificationEnumToValue(Classification? classification) {
            switch (classification) {
                case Classification.Public:
                    return 1;
                case Classification.Internal:
                    return 2;
                case Classification.Confidential:
                    return 3;
                case Classification.StrictlyConfidential:
                    return 4;
                default:
                    return null;
            }
        }

        public static string ConvertGroupMemberAcceptanceToValue(GroupMemberAcceptance? gma) {
            switch (gma) {
                case GroupMemberAcceptance.AutoAllow:
                    return "autoallow";
                case GroupMemberAcceptance.Pending:
                    return "pending";
                default:
                    return null;
            }
        }

        public static string ConvertResolutionStrategyToValue(ResolutionStrategy strategy) {
            switch (strategy) {
                case ResolutionStrategy.AutoRename:
                    return "autorename";
                case ResolutionStrategy.Fail:
                    return "fail";
                case ResolutionStrategy.Overwrite:
                    return "overwrite";
                default:
                    return null;
            }
        }

        public static PasswordCharacterSetType ConvertValueToCharacterSetTypeEnum(string value) {
            switch (value) {
                case "none":
                    return PasswordCharacterSetType.None;
                case "uppercase":
                    return PasswordCharacterSetType.Uppercase;
                case "lowercase":
                    return PasswordCharacterSetType.Lowercase;
                case "numeric":
                    return PasswordCharacterSetType.Numeric;
                case "special":
                    return PasswordCharacterSetType.Special;
                default:
                    return PasswordCharacterSetType.None;
            }
        }

        public static Func<string, UserType> ConvertValueToUserTypeEnum = value => {
            switch (value) {
                case "internal":
                    return UserType.Internal;
                case "external":
                    return UserType.External;
                case "deleted":
                    return UserType.Deleted;
                default:
                    return UserType.System;
            }
        };
    }
}