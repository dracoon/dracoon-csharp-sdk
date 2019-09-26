using Dracoon.Sdk.Filter;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.Sort;

namespace Dracoon.Sdk {
    /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iShares"]/IShares/*'/>
    public interface IShares {
        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iShares"]/CreateDownloadShare/*'/>
        DownloadShare CreateDownloadShare(CreateDownloadShareRequest request);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iShares"]/GetDownloadShares/*'/>
        DownloadShareList GetDownloadShares(long? offset = null, long? limit = null, GetDownloadSharesFilter filter = null, SharesSort sort = null);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iShares"]/DeleteDownloadShare/*'/>
        void DeleteDownloadShare(long shareId);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iShares"]/CreateUploadShare/*'/>
        UploadShare CreateUploadShare(CreateUploadShareRequest request);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iShares"]/GetUploadShares/*'/>
        UploadShareList GetUploadShares(long? offset = null, long? limit = null, GetUploadSharesFilter filter = null, SharesSort sort = null);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iShares"]/DeleteUploadShare/*'/>
        void DeleteUploadShare(long shareId);
    }
}