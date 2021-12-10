
namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores informations about the infrastructure configuration of the server.
    /// </summary>
    public class ServerInfrastructureSettings {

        /// <summary>
        ///     Is <c>true</c> if share passwords can be send via SMS. Otherwise <c>false</c>.
        /// </summary>
        public bool SmsConfigEnabled { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if the media server is available. Otherwise <c>false</c>.
        /// </summary>
        public bool MediaServerConfigEnabled { get; internal set; }

        /// <summary>
        ///     The suggested S3 region.
        /// </summary>
        public string S3DefaultRegion { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if s3 direct upload must be used and a normal proxied upload isn possible. Otherwise <c>false</c>.
        /// </summary>
        public bool S3EnforceDirectUpload { get; internal set; }

        /// <summary>
        ///     Determines if the DRACOON Core is deployed in the cloud environment.
        /// </summary>
        public bool IsDracoonCloud { get; internal set; }

        /// <summary>
        ///     Current tenant UUID.
        /// </summary>
        public string TenantUUID { get; internal set; }
    }
}
