using Android.Net;
using Javax.Net.Ssl;
using System.Net.Http;
using TrireksaMobile.Droid;
using TrireksaMobile.Helpers;
using Xamarin.Android.Net;

[assembly: Xamarin.Forms.Dependency(typeof(HTTPClientHandlerCreationServiceAndroid))]
namespace TrireksaMobile.Droid
{
    public class HTTPClientHandlerCreationServiceAndroid : IHTTPClientHandlerCreationService
    {
        public HttpClientHandler GetInsecureHandler()
        {
            return new IgnoreSSLClientHandler();
        }
    }

    public class IgnoreSSLClientHandler : AndroidClientHandler
    {
        protected override SSLSocketFactory ConfigureCustomSSLSocketFactory(HttpsURLConnection connection)
        {
            return SSLCertificateSocketFactory.GetInsecure(1000, null);
        }

        protected override IHostnameVerifier GetSSLHostnameVerifier(HttpsURLConnection connection)
        {
            return new IgnoreSSLHostnameVerifier();
        }
    }

    public class IgnoreSSLHostnameVerifier : Java.Lang.Object, IHostnameVerifier
    {
        public bool Verify(string hostname, ISSLSession session)
        {
            return true;
        }
    }
}