using Dracoon.Sdk.Model;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Dracoon.Sdk.Example {
    public class OAuthExamples {

        private static readonly Uri SERVER_URI = new Uri("https://dracoon.team");
        private static readonly string CLIENT_ID = "client-id";
        private static readonly string CLIENT_SECRET = "client-secret";
        private static readonly int REDIRECT_PORT = 10000;

        [STAThread]
        private static void Main() {
            // Authorize client
            string authCode = AuthorizeClient();

            // Initialize dracoon client
            DracoonClient client = CreateClient(authCode);

            // Use client
            UseClient(client);

            // After first usage of client the access and refresh tokens are retrieved and can be persisted
            string accessToken = client.Auth.AccessToken;
            string refreshToken = client.Auth.RefreshToken;

            // On next startup of your application you can then use the tokens for another initialization without users browser login
            client = CreateClient(accessToken, refreshToken);

            // Use client again with new initialization
            UseClient(client);
        }

        private static string AuthorizeClient() {
            string state = GenerateRandomBase64(32);

            // Create authorization uri
            Uri authUrl = OAuthHelper.CreateAuthorizationUrl(SERVER_URI, CLIENT_ID, state);

            // Open authorization URL in user's browser and wait for callback
            Uri loginResultUri = Login(authUrl).Result;

            // Extract the state and code from callback uri
            string callbackState = OAuthHelper.ExtractAuthorizationStateFromUri(loginResultUri);
            string callbackCode = OAuthHelper.ExtractAuthorizationCodeFromUri(loginResultUri);

            // Check state
            if (!state.Equals(callbackState)) {
                throw new Exception("Received OAuth state is not the same as expected!");
            }
            return callbackCode;
        }

        private async static Task<Uri> Login(Uri authUrl) {
            using (HttpListener callbackListener = new HttpListener()) {
                callbackListener.Prefixes.Add("http://localhost:" + REDIRECT_PORT + "/");
                callbackListener.Start();
                System.Diagnostics.Process.Start(authUrl.ToString());
                HttpListenerContext context = await callbackListener.GetContextAsync();
                return context.Request.Url;
            }
        }

        private static DracoonClient CreateClient(string authCode) {
            // Init the auth object with the authCode
            DracoonAuth auth = new DracoonAuth(CLIENT_ID, CLIENT_SECRET, authCode);

            // Create a dracoon client with default settings but with authorization
            return new DracoonClient(SERVER_URI, auth);
        }

        private static DracoonClient CreateClient(string accessToken, string refreshToken) {
            // Init the auth object with the access and refresh token
            DracoonAuth auth = new DracoonAuth(CLIENT_ID, CLIENT_SECRET, accessToken, refreshToken);

            // Create a dracoon client with default settings but with authorization
            return new DracoonClient(SERVER_URI, auth);
        }

        private static void UseClient(DracoonClient client) {
            NodeList rootNodes = client.Nodes.GetNodes();
            foreach (Node current in rootNodes.Items) {
                Console.WriteLine("NodeId: " + current.Id + "; NodeName: " + current.Name);
            }
        }

        private static string GenerateRandomBase64(int length) {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[length];
            rng.GetBytes(randomBytes);
            return EncodeByteArrayToUrlEncodedBase64(randomBytes);
        }

        internal static string EncodeByteArrayToUrlEncodedBase64(byte[] bytes) {
            string base64 = Convert.ToBase64String(bytes);
            base64 = base64.Replace("+", "-");
            base64 = base64.Replace("/", "_");
            base64 = base64.Replace("=", "");
            return base64;
        }
    }
}
