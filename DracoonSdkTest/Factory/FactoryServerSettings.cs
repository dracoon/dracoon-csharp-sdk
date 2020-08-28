using Dracoon.Crypto.Sdk;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Settings;
using System.Collections.Generic;

namespace Dracoon.Sdk.UnitTest.Factory {
    internal class FactoryServerSettings {
        internal static ServerGeneralSettings ServerGeneralSettings {
            get {
                return new ServerGeneralSettings {
                    CryptoEnabled = true,
                    EmailNotificationButtonEnabled = false,
                    EulaEnabled = true,
                    MediaServerEnabled = false,
                    SharePasswordSmsEnabled = true,
                    UseS3Storage = false,
                    WeakPasswordEnabled = true
                };
            }
        }

        internal static ApiGeneralSettings ApiGeneralSettings {
            get {
                return new ApiGeneralSettings {
                    CryptoEnabled = true,
                    EmailNotificationButtonEnabled = false,
                    EulaEnabled = true,
                    MediaServerEnabled = false,
                    SharePasswordSmsEnabled = true,
                    UseS3Storage = false,
                    WeakPasswordEnabled = true
                };
            }
        }

        internal static ServerInfrastructureSettings ServerInfrastructureSettings {
            get {
                return new ServerInfrastructureSettings {
                    MediaServerConfigEnabled = true,
                    S3DefaultRegion = "DE",
                    SmsConfigEnabled = true,
                    S3EnforceDirectUpload = false
                };
            }
        }

        internal static List<FileKeyAlgorithm> FileKeyAlgorithms {
            get {
                return new List<FileKeyAlgorithm> {
                    new FileKeyAlgorithm {
                        Algorithm = EncryptedFileKeyAlgorithm.RSA2048_AES256GCM,
                        State = AlgorithmState.Discouraged
                    },
                    new FileKeyAlgorithm {
                        Algorithm = EncryptedFileKeyAlgorithm.RSA4096_AES256GCM,
                        State = AlgorithmState.Required
                    }
                };
            }
        }

        internal static List<UserKeyPairAlgorithmData> UserKeyPairAlgorithms {
            get {
                return new List<UserKeyPairAlgorithmData> {
                    new UserKeyPairAlgorithmData {
                        Algorithm = UserKeyPairAlgorithm.RSA2048,
                        State = AlgorithmState.Discouraged
                    },
                    new UserKeyPairAlgorithmData {
                        Algorithm = UserKeyPairAlgorithm.RSA4096,
                        State = AlgorithmState.Required
                    }
                };
            }
        }

        internal static ApiAlgorithms ApiAlgorithms {
            get {
                return new ApiAlgorithms {
                    FileKeyAlgorithms = new List<ApiAlgorithm> {
                        new ApiAlgorithm {
                            Version = "RSA-4096/AES-256-GCM",
                            Status = "REQUIRED",
                            Description = "RSA-4096/AES-256-GCM"
                        },
                        new ApiAlgorithm {
                            Version = "A",
                            Status = "DISCOURAGED",
                            Description = "RSA-2048/AES-256-GCM"
                        }
                    },
                    KeyPairAlgorithms = new List<ApiAlgorithm> {
                        new ApiAlgorithm {
                            Version = "RSA-4096",
                            Status = "REQUIRED",
                            Description = "RSA-4096-OAEP-SHA256MGFSHA256"
                        },
                        new ApiAlgorithm {
                            Version = "A",
                            Status = "DISCOURAGED",
                            Description = "RSA-2048-OAEP-SHA256MGF1"
                        }
                    }
                };
            }
        }

        internal static ApiInfrastructureSettings ApiInfrastructureSettings {
            get {
                return new ApiInfrastructureSettings {
                    MediaServerConfigEnabled = true,
                    S3DefaultRegion = "DE",
                    SmsConfigEnabled = true
                };
            }
        }

        internal static ServerDefaultSettings ServerDefaultSettings {
            get {
                return new ServerDefaultSettings {
                    DownloadShareDefaultExpirationPeriodInDays = 5,
                    FileUploadDefaultExpirationPeriodInDays = 4,
                    LanguageDefault = "DE",
                    UploadShareDefaultExpirationPeriodInDays = 7
                };
            }
        }

        internal static ApiDefaultsSettings ApiDefaultsSettings {
            get {
                return new ApiDefaultsSettings {
                    DownloadShareDefaultExpirationPeriodInDays = 5,
                    FileUploadDefaultExpirationPeriodInDays = 4,
                    LanguageDefault = "DE",
                    UploadShareDefaultExpirationPeriodInDays = 7
                };
            }
        }
    }
}