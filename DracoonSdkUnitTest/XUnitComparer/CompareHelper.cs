using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace Dracoon.Sdk.UnitTest.XUnitComparer {
    internal static class CompareHelper {

        internal static bool ListIsEqual(List<long> x, List<long> y) {
            if (x == null && y == null) {
                return true;
            }

            if (x == null && y != null) {
                return false;
            }

            if (x != null && y == null) {
                return false;
            }

            if (x.Count != y.Count) {
                return false;
            }

            for (int i = 0; i < x.Count; i++) {
                if (x[i] != y[i]) {
                    return false;
                }
            }

            return true;
        }

        internal static bool ListIsEqual(List<string> x, List<string> y) {
            if (x == null && y == null) {
                return true;
            }

            if (x == null && y != null) {
                return false;
            }

            if (x != null && y == null) {
                return false;
            }

            if (x.Count != y.Count) {
                return false;
            }

            for (int i = 0; i < x.Count; i++) {
                if (x[i] != y[i]) {
                    return false;
                }
            }

            return true;
        }

        internal static bool ListIsEqual(List<UserRole> x, List<UserRole> y) {
            if (x == null && y == null) {
                return true;
            }

            if (x == null && y != null) {
                return false;
            }

            if (x != null && y == null) {
                return false;
            }
            if (x.Count != y.Count) {
                return false;
            }
            for (int i = 0; i < x.Count; i++) {
                if (x[i] != y[i]) {
                    return false;
                }
            }
            return true;
        }

        internal static bool ListIsEqual(List<UserGroup> x, List<UserGroup> y) {
            if (x == null && y == null) {
                return true;
            }

            if (x == null && y != null) {
                return false;
            }

            if (x != null && y == null) {
                return false;
            }
            if (x.Count != y.Count) {
                return false;
            }
            UserGroupComparer comparer = new UserGroupComparer();
            for (int i = 0; i < x.Count; i++) {
                if (!comparer.Equals(x[i], y[i])) {
                    return false;
                }
            }
            return true;
        }

        internal static bool ListIsEqual(List<Node> x, List<Node> y) {
            if (x == null && y == null) {
                return true;
            }

            if (x == null && y != null) {
                return false;
            }

            if (x != null && y == null) {
                return false;
            }
            if (x.Count != y.Count) {
                return false;
            }
            NodeComparer comparer = new NodeComparer();
            for (int i = 0; i < x.Count; i++) {
                if (!comparer.Equals(x[i], y[i])) {
                    return false;
                }
            }
            return true;
        }

        internal static bool ListIsEqual(List<ApiCopyNode> x, List<ApiCopyNode> y) {
            if (x == null && y == null) {
                return true;
            }

            if (x == null && y != null) {
                return false;
            }

            if (x != null && y == null) {
                return false;
            }
            if (x.Count != y.Count) {
                return false;
            }
            ApiCopyNodeComparer comparer = new ApiCopyNodeComparer();
            for (int i = 0; i < x.Count; i++) {
                if (!comparer.Equals(x[i], y[i])) {
                    return false;
                }
            }
            return true;
        }

        internal static bool ListIsEqual(List<ApiMoveNode> x, List<ApiMoveNode> y) {
            if (x == null && y == null) {
                return true;
            }

            if (x == null && y != null) {
                return false;
            }

            if (x != null && y == null) {
                return false;
            }
            if (x.Count != y.Count) {
                return false;
            }
            ApiMoveNodeComparer comparer = new ApiMoveNodeComparer();
            for (int i = 0; i < x.Count; i++) {
                if (!comparer.Equals(x[i], y[i])) {
                    return false;
                }
            }
            return true;
        }

        internal static bool ListIsEqual(List<RecycleBinItem> x, List<RecycleBinItem> y) {
            if (x == null && y == null) {
                return true;
            }

            if (x == null && y != null) {
                return false;
            }

            if (x != null && y == null) {
                return false;
            }
            if (x.Count != y.Count) {
                return false;
            }
            RecycleBinItemComparer comparer = new RecycleBinItemComparer();
            for (int i = 0; i < x.Count; i++) {
                if (!comparer.Equals(x[i], y[i])) {
                    return false;
                }
            }
            return true;
        }

        internal static bool ListIsEqual(List<PreviousVersion> x, List<PreviousVersion> y) {
            if (x == null && y == null) {
                return true;
            }

            if (x == null && y != null) {
                return false;
            }

            if (x != null && y == null) {
                return false;
            }
            if (x.Count != y.Count) {
                return false;
            }
            PreviousVersionComparer comparer = new PreviousVersionComparer();
            for (int i = 0; i < x.Count; i++) {
                if (!comparer.Equals(x[i], y[i])) {
                    return false;
                }
            }
            return true;
        }

        internal static bool ListIsEqual(List<DownloadShare> x, List<DownloadShare> y) {
            if (x == null && y == null) {
                return true;
            }

            if (x == null && y != null) {
                return false;
            }

            if (x != null && y == null) {
                return false;
            }
            if (x.Count != y.Count) {
                return false;
            }
            DownloadShareComparer comparer = new DownloadShareComparer();
            for (int i = 0; i < x.Count; i++) {
                if (!comparer.Equals(x[i], y[i])) {
                    return false;
                }
            }
            return true;
        }

        internal static bool ListIsEqual(List<UploadShare> x, List<UploadShare> y) {
            if (x == null && y == null) {
                return true;
            }

            if (x == null && y != null) {
                return false;
            }

            if (x != null && y == null) {
                return false;
            }
            if (x.Count != y.Count) {
                return false;
            }
            UploadShareComparer comparer = new UploadShareComparer();
            for (int i = 0; i < x.Count; i++) {
                if (!comparer.Equals(x[i], y[i])) {
                    return false;
                }
            }
            return true;
        }

        internal static bool ListIsEqual(List<Parameter> x, List<Parameter> y) {
            if (x == null && y == null) {
                return true;
            }

            if (x == null && y != null) {
                return false;
            }

            if (x != null && y == null) {
                return false;
            }
            if (x.Count != y.Count) {
                return false;
            }
            for (int i = 0; i < x.Count; i++) {
                if (!x[i].Name.Equals(y[i].Name) || !x[i].Value.Equals(y[i].Value)) {
                    return false;
                }
            }
            return true;
        }

        internal static bool ListIsEqual(List<Attribute> x, List<Attribute> y) {
            if (x == null && y == null) {
                return true;
            }

            if (x == null && y != null) {
                return false;
            }

            if (x != null && y == null) {
                return false;
            }
            if (x.Count != y.Count) {
                return false;
            }

            for (int i = 0; i < x.Count; i++) {
                if (!x[i].Key.Equals(y[i].Key) || !x[i].Value.Equals(y[i].Value)) {
                    return false;
                }
            }

            return true;
        }

        internal static bool ListIsEqual(List<ApiAttribute> x, List<ApiAttribute> y) {
            if (x == null && y == null) {
                return true;
            }

            if (x == null && y != null) {
                return false;
            }

            if (x != null && y == null) {
                return false;
            }
            if (x.Count != y.Count) {
                return false;
            }

            for (int i = 0; i < x.Count; i++) {
                if (!x[i].Key.Equals(y[i].Key) || !x[i].Value.Equals(y[i].Value)) {
                    return false;
                }
            }

            return true;
        }

        internal static bool ListIsEqual(WebHeaderCollection x, WebHeaderCollection y) {
            if (x == null && y == null) {
                return true;
            }

            if (x == null && y != null) {
                return false;
            }

            if (x != null && y == null) {
                return false;
            }
            if (x.Count != y.Count) {
                return false;
            }
            for (int i = 0; i < x.Count; i++) {
                if (x[i] != y[i]) {
                    return false;
                }
            }
            return true;
        }

        internal static bool ListIsEqual(List<PasswordCharacterSet> x, List<PasswordCharacterSet> y) {
            if (x == null && y == null) {
                return true;
            }

            if (x == null && y != null) {
                return false;
            }

            if (x != null && y == null) {
                return false;
            }
            if (x.Count != y.Count) {
                return false;
            }
            PasswordCharacterSetComparer comparer = new PasswordCharacterSetComparer();
            for (int i = 0; i < x.Count; i++) {
                if (!comparer.Equals(x[i], y[i])) {
                    return false;
                }
            }
            return true;
        }

    }
}
