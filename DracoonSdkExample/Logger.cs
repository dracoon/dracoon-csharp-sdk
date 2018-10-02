using System;

namespace Dracoon.Sdk.Example {
    public class Logger : ILog {

        public void Debug(string tag, string message) {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Debug(string tag, string message, Exception e) {
            System.Diagnostics.Debug.WriteLine(message);
            System.Diagnostics.Debug.WriteLine(e.StackTrace);
        }

        public void Error(string tag, string message) {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Error(string tag, string message, Exception e) {
            System.Diagnostics.Debug.WriteLine(message);
            System.Diagnostics.Debug.WriteLine(e.StackTrace);
        }

        public void Info(string tag, string message) {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Info(string tag, string message, Exception e) {
            System.Diagnostics.Debug.WriteLine(message);
            System.Diagnostics.Debug.WriteLine(e.StackTrace);
        }

        public void Warn(string tag, string message) {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Warn(string tag, string message, Exception e) {
            System.Diagnostics.Debug.WriteLine(message);
            System.Diagnostics.Debug.WriteLine(e.StackTrace);
        }
    }
}
