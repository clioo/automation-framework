using CredentialManagement;
using System.Collections.Generic;

namespace HttpUtiityTests.CredentialManager
{
    public static class CredentialRetriever
    {
        static public IDictionary<string, string> GetCredentials(string target = "http://co-svr-tfs:8080/")
        {
            using(var credential = new Credential())
            {
                IDictionary<string, string> user = new Dictionary<string, string>();
                credential.Target = target;
                credential.Load();

                user.Add("username", credential.Username);
                user.Add("password", credential.Password);
                return user;
            }
        }
    }
}
