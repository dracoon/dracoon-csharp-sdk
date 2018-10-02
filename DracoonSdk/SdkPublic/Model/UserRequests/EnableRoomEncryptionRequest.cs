
namespace Dracoon.Sdk.Model {
    /// <include file="UserRequestsDoc.xml" path='docs/members[@name="enableRoomEncryptionRequest"]/EnableRoomEncryptionRequest/*'/>
    public class EnableRoomEncryptionRequest {

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="enableRoomEncryptionRequest"]/Id/*'/>
        public long Id {
            get; private set;
        }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="enableRoomEncryptionRequest"]/IsEncryptionEnabled/*'/>
        public bool IsEncryptionEnabled {
            get; private set;
        }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="enableRoomEncryptionRequest"]/UseDataSpaceRescueKey/*'/>
        public bool UseDataSpaceRescueKey {
            get; set;
        }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="enableRoomEncryptionRequest"]/DataRoomRescueKeyPassword/*'/>
        public string DataRoomRescueKeyPassword {
            get; set;
        }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="enableRoomEncryptionRequest"]/EnableRoomEncryptionRequestConstructor/*'/>
        public EnableRoomEncryptionRequest(long id, bool isEncryptionEnabled, bool useDataSpaceRescueKey = false, string dataRoomRescueKeyPassword = null) {
            Id = id;
            IsEncryptionEnabled = isEncryptionEnabled;
            UseDataSpaceRescueKey = useDataSpaceRescueKey;
            DataRoomRescueKeyPassword = dataRoomRescueKeyPassword;
        }
    }
}
