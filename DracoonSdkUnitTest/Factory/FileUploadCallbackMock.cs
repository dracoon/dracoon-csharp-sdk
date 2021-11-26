using Dracoon.Sdk.Error;
using Dracoon.Sdk.Model;

namespace Dracoon.Sdk.UnitTest.Factory {
    public class FileUploadCallbackMock : IFileUploadCallback {
        public void OnStarted(string actionId) {
        }

        public void OnRunning(string actionId, long bytesUploaded, long bytesTotal) {
        }

        public void OnFinished(string actionId, Node resultNode) {
        }

        public void OnCanceled(string actionId) {
        }

        public void OnFailed(string actionId, DracoonException occuredError) {
        }
    }
}