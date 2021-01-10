using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TrireksaMobile.Helpers
{
    public interface IHTTPClientHandlerCreationService
    {
        HttpClientHandler GetInsecureHandler();
    }
}
