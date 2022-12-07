using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Http;
using Google.Apis.Requests;
using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using WebTech.L2F.Infrastructure;
using WebTech.L2F.Services;
using static WebTech.L2F.Constants.Strings;

namespace WebTech.L2F.AppCore
{
    public class RealtimeDatabaseContext : IFirebaseContext
    {
        private string _projectId, _customToken, _apiKey;
        private RealtimeDatabaseService _realtimeDBService = new RealtimeDatabaseService();

        public RealtimeDatabaseContext(string serviceAccountJSON, string apiKey)
        {
            this._projectId = JsonSerializer.Deserialize<Dictionary<string, string>>(serviceAccountJSON)[PROJECT_ID];
            this._apiKey = apiKey;

            FirebaseApp.Create(new AppOptions() {
                Credential = GoogleCredential.FromJson(serviceAccountJSON)
            });
        }

        public IRealtimeDatabaseRecord Retrieve(string jsonPath) => _realtimeDBService.Retrieve(this._projectId, _customToken, jsonPath);

        public async void SignInUser(string uid)
        {
            var isSignedIn = false;

            try
            {
                _customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(uid);

                if (string.IsNullOrEmpty(_customToken))
                    throw new ArgumentException("Signing in failed.");
                
                var reqBuilder = new RequestBuilder() {
                    Method = HttpConsts.Post,
                    BaseUri = new Uri("https://identitytoolkit.googleapis.com/v1/accounts:signInWithCustomToken")
                };
                var httpRequest = new HttpRequestMessage();

                httpRequest.Method = HttpMethod.Post;
                httpRequest.RequestUri = new Uri("https://identitytoolkit.googleapis.com/v1/accounts:signInWithCustomToken");
                httpRequest.RequestUri = new Uri(httpRequest.RequestUri, $"?key={_apiKey}");
                
                reqBuilder.AddParameter(RequestParameterType.Query, "key", _apiKey);

                var request = reqBuilder.CreateRequest();
            }

            catch (FirebaseException ex)
            {
                throw ex;
            }
        }
    }
}