
using Dracoon.Sdk.Error;

namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="iFileDownloadCallback"]/IFileDownloadCallback/*'/>
    public interface IFileDownloadCallback {

        /// <include file = "ModelDoc.xml" path='docs/members[@name="iFileDownloadCallback"]/OnStarted/*'/>
        void OnStarted(string actionId);

        /// <include file = "ModelDoc.xml" path='docs/members[@name="iFileDownloadCallback"]/OnRunning/*'/>
        void OnRunning(string actionId, long bytesDownloaded, long bytesTotal);

        /// <include file = "ModelDoc.xml" path='docs/members[@name="iFileDownloadCallback"]/OnFinished/*'/>
        void OnFinished(string actionId);

        /// <include file = "ModelDoc.xml" path='docs/members[@name="iFileDownloadCallback"]/OnCanceled/*'/>
        void OnCanceled(string actionId);

        /// <include file = "ModelDoc.xml" path='docs/members[@name="iFileDownloadCallback"]/OnFailed/*'/>
        void OnFailed(string actionId, DracoonException occuredError);
    }
}
