using System.Collections.Generic;

namespace Dracoon.Sdk.Model {

    /// <include file = "ModelDoc.xml" path='docs/members[@name="recycleBinItemList"]/RecycleBinItemList/*'/>
    public class RecycleBinItemList {

        /// <include file = "ModelDoc.xml" path='docs/members[@name="recycleBinItemList"]/Offset/*'/>
        public long Offset {
            get; internal set;
        }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="recycleBinItemList"]/Limit/*'/>
        public long Limit {
            get; internal set;
        }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="recycleBinItemList"]/Total/*'/>
        public long Total {
            get; internal set;
        }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="recycleBinItemList"]/Items/*'/>
        public List<RecycleBinItem> Items {
            get; internal set;
        }
    }
}
