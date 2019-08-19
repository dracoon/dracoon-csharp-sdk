using Dracoon.Sdk.Error;
using Dracoon.Sdk.Model;
using System;

namespace Dracoon.Sdk.UnitTest.Factory {
    public class FileDownloadCallbackMock : IFileDownloadCallback {
        public void OnStarted(string actionId) {
        }

        public void OnRunning(string actionId, long bytesDownloaded, long bytesTotal) {
        }

        public void OnFinished(string actionId) {
        }

        public void OnCanceled(string actionId) {
        }

        public void OnFailed(string actionId, DracoonException occuredError) {
        }
    }
}