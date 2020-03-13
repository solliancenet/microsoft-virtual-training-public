using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Web.Configuration;

namespace InsuranceAPI
{
    public class Util
    {

        public static string EncryptSecret;

        public static async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(WebConfigurationManager.AppSettings["ClientId"],
                        WebConfigurationManager.AppSettings["ClientSecret"]);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }
    }
}