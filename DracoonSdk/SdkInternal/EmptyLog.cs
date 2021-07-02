using System;

namespace Dracoon.Sdk.SdkInternal {
    internal class EmptyLog : ILog {
        public void Debug(string tag, string message) {
            // Default: Log nothing
        }

        public void Debug(string tag, string message, Exception e) {
            // Default: Log nothing
        }

        public void Error(string tag, string message) {
            // Default: Log nothing
        }

        public void Error(string tag, string message, Exception e) {
            // Default: Log nothing
        }

        public void Info(string tag, string message) {
            // Default: Log nothing
        }

        public void Info(string tag, string message, Exception e) {
            // Default: Log nothing
        }

        public void Warn(string tag, string message) {
            // Default: Log nothing
        }

        public void Warn(string tag, string message, Exception e) {
            // Default: Log nothing
        }
    }
}