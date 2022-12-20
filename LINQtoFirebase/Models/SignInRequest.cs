using System;

namespace WebTech.L2F.AppCore
{
    public class SignInRequest
    {
        public string token { get; set; }
        public bool returnSecureToken { get; set; }
        public string tenantId { get; set; }
    }
}