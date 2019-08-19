using System;

namespace Dracoon.Sdk.SdkInternal {
    internal class EmptyLog : ILog {
        public void Debug(string tag, string message) { }

        public void Debug(string tag, string message, Exception e) { }

        public void Error(string tag, string message) { }

        public void Error(string tag, string message, Exception e) { }

        public void Info(string tag, string message) { }

        public void Info(string tag, string message, Exception e) { }

        public void Warn(string tag, string message) { }

        public void Warn(string tag, string message, Exception e) { }
    }
}