using System;

namespace WebTech.L2F.AppCore
{
    public class SignInResponse
    {
        public string kind { get; set; }
        public string idToken { get; set; }
        public string refreshToken { get; set; }
        public string expiresIn { get; set; }
        public bool isNewUser { get; set; }
    }
}