namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="serverInfrastructureSettings"]/serverInfrastructureSettings/*'/>
    public class ServerInfrastructureSettings {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="serverInfrastructureSettings"]/SmsConfigEnabled/*'/>
        public bool SmsConfigEnabled { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="serverInfrastructureSettings"]/MediaServerConfigEnabled/*'/>
        public bool MediaServerConfigEnabled { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="serverInfrastructureSettings"]/S3DefaultRegion/*'/>
        public string S3DefaultRegion { get; internal set; }
    }
}