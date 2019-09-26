namespace Dracoon.Sdk.SdkInternal.OAuth {
    internal interface IOAuth {
        DracoonAuth Auth { get; set; }
        string BuildAuthString();

        void RefreshAccessToken();
    }
}