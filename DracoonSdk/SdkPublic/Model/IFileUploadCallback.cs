using Dracoon.Sdk.Error;

namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="iFileUploadCallback"]/IFileUploadCallback/*'/>
    public interface IFileUploadCallback {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="iFileUploadCallback"]/OnStarted/*'/>
        void OnStarted(string actionId);

        /// <include file = "ModelDoc.xml" path='docs/members[@name="iFileUploadCallback"]/OnRunning/*'/>
        void OnRunning(string actionId, long bytesUploaded, long bytesTotal);

        /// <include file = "ModelDoc.xml" path='docs/members[@name="iFileUploadCallback"]/OnFinished/*'/>
        void OnFinished(string actionId, Node resultNode);

        /// <include file = "ModelDoc.xml" path='docs/members[@name="iFileUploadCallback"]/OnCanceled/*'/>
        void OnCanceled(string actionId);

        /// <include file = "ModelDoc.xml" path='docs/members[@name="iFileUploadCallback"]/OnFailed/*'/>
        void OnFailed(string actionId, DracoonException occuredError);
    }
}