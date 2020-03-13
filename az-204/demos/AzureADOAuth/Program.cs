using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureADOAuth
{
    class Program
    {
        //To learn how to register a client app and get a Client ID, see https://msdn.microsoft.com/library/azure/mt403303.aspx#clientID     
        static string clientId = "YOURID";
        static string clientSecret = "YOURSECRET";
        static string resourceUri = "https://datacatalog.azure.com";
        static string redirectUri = "https://login.live.com/oauth20_desktop.srf";
        static string authorityUri = "https://login.windows.net/common/oauth2/authorize";

        static void Main(string[] args)
        {
            //resourceUri = "https://datacatalog.azure.com";
            resourceUri = "https://graph.microsoft.com";

            AuthenticationResult ar = AccessToken().Result;
            Console.WriteLine(ar.AccessToken);
        }

        static async Task<AuthenticationResult> AccessToken()
        {
            ClientCredential cc = new ClientCredential(clientId, clientSecret);

            // Create an instance of AuthenticationContext to acquire an Azure access token  
            // OAuth2 authority Uri  
            string authorityUri = "https://login.windows.net/common/oauth2/authorize";
            AuthenticationContext authContext = new AuthenticationContext(authorityUri);

            // Call AcquireToken to get an Azure token from Azure Active Directory token issuance endpoint  
            //  AcquireToken takes a Client Id that Azure AD creates when you register your client app.  
            return authContext.AcquireTokenAsync(resourceUri, cc).Result;
        }
    }
}
    