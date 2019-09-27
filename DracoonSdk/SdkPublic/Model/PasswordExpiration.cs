namespace Dracoon.Sdk.Model {
    public class PasswordExpiration {
        public bool IsEnabled { get; internal set; }
        public int ExpiresAfterDays { get; internal set; }
    }
}