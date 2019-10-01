namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordLoginFailurePolicies"]/PasswordLoginFailurePolicies/*'/>
    public class PasswordLoginFailurePolicies {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordLoginFailurePolicies"]/IsEnabled/*'/>
        public bool IsEnabled { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordLoginFailurePolicies"]/MaximumNumberOfLoginFailures/*'/>
        public int MaximumNumberOfLoginFailures { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordLoginFailurePolicies"]/LoginRetryPeriodMinutes/*'/>
        public int LoginRetryPeriodMinutes { get; internal set; }
    }
}