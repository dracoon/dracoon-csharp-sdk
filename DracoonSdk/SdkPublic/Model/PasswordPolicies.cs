namespace Dracoon.Sdk.Model {
    public class PasswordPolicies {
        public PasswordLoginPolicies LoginPolicies { get; internal set; }
        public PasswordSharePolicies SharePolicies { get; internal set; }
        public PasswordEncryptionPolicies EncryptionPolicies { get; internal set; }
    }
}