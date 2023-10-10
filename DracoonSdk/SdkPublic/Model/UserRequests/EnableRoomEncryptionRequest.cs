using Dracoon.Crypto.Sdk;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Request to enable the encryption in the specified room.
    /// </summary>
    public class EnableRoomEncryptionRequest {
        /// <summary>
        ///     The room id for which the encryption should be enabled.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        ///     If set to <c>true</c> the encryption for the room will be enabled, otherwise the encryption of the will be disabled.
        /// </summary>
        public bool IsEncryptionEnabled { get; private set; }

        /// <summary>
        ///     If set to <c>true</c> the rescue key of the dataspace will be usable at the room.
        /// </summary>
        public bool UseDataSpaceRescueKey { get; set; }

        /// <summary>
        ///     If a room rescue key should be set you have to specify a private key password for the new key pair.
        /// </summary>
        public string DataRoomRescueKeyPassword { get; set; }

        /// <summary>
        ///     The algorithm for the key pair which should be used as rescue key pair.
        /// </summary>
        public UserKeyPairAlgorithm? DataRoomRescueKeyPairAlgorithm { get; set; }

        /// <summary>
        ///     Constructs a new enable room encryption request.
        /// </summary>
        /// <param name="id"><see cref="Id"/></param>
        /// <param name="isEncryptionEnabled"><see cref="IsEncryptionEnabled"/></param>
        /// <param name="useDataSpaceRescueKey"><see cref="UseDataSpaceRescueKey"/></param>
        /// <param name="dataRoomRescueKeyPassword"><see cref="DataRoomRescueKeyPassword"/></param>
        /// <param name="dataRoomRescueKeyPairAlgorithm"><see cref="DataRoomRescueKeyPairAlgorithm"/></param>
        public EnableRoomEncryptionRequest(long id, bool isEncryptionEnabled, bool useDataSpaceRescueKey = false,
            string dataRoomRescueKeyPassword = null, UserKeyPairAlgorithm? dataRoomRescueKeyPairAlgorithm = null) {
            Id = id;
            IsEncryptionEnabled = isEncryptionEnabled;
            UseDataSpaceRescueKey = useDataSpaceRescueKey;
            DataRoomRescueKeyPassword = dataRoomRescueKeyPassword;
            DataRoomRescueKeyPairAlgorithm = dataRoomRescueKeyPairAlgorithm;
        }
    }
}