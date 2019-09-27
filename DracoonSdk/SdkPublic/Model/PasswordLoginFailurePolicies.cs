namespace Dracoon.Sdk.Model {
    public class PasswordLoginFailurePolicies {
        public bool IsEnabled { get; internal set; }
        public int MaximumNumberOfLoginFailures { get; internal set; }
        public int LoginRetryPeriodMinutes { get; internal set; }
    }
}