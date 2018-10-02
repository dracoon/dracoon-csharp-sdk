using Dracoon.Sdk.Model;
using System;
using System.Net;

namespace Dracoon.Sdk.SdkInternal.Util {
    internal class DracoonWebClientExtension : WebClient {

        private readonly long? rangeFrom;
        private readonly long? rangeTo;
        private DracoonHttpConfig config;

        public DracoonWebClientExtension(long? rangeFrom = null, long? rangeTo = null) {
            this.rangeFrom = rangeFrom;
            this.rangeTo = rangeTo;
        }

        public void SetHttpConfigParams(DracoonHttpConfig httpConfig) {
            config = httpConfig;
        }

        protected override WebRequest GetWebRequest(Uri address) {
            HttpWebRequest request = (HttpWebRequest) base.GetWebRequest(address);
            if (rangeFrom.HasValue && rangeTo.HasValue) {
                request.AddRange(rangeFrom.Value, rangeTo.Value);
            }
            request.ReadWriteTimeout = config.ReadWriteTimeout;
                request.Timeout = config.ConnectionTimeout;
            if (config.WebProxy != null) {
                request.Proxy = config.WebProxy;
            }
            return request;
        }
    }
}
