using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using WebTech.L2F.Infrastructure;
using static WebTech.L2F.Constants.Strings;

namespace WebTech.L2F.Services
{
    public class RealtimeDatabaseService
    {
        private static HttpClient _httpClient = new HttpClient();

        public IRealtimeDatabaseRecord Retrieve(string projectId, string customToken, string jsonPath)
        {
            jsonPath = (char.Equals(jsonPath[0], '/')) ? jsonPath.Substring(1, jsonPath.Length - 1) : jsonPath;
            jsonPath = string.Equals(jsonPath.Substring((jsonPath.Length - 5), 5), ".json") ?  jsonPath.Substring(0, (jsonPath.Length - 5)) : jsonPath;

            var endpoint = string.Format(FIREBASE_REALTIME_DB_ENDPOINT_FORMAT, projectId, customToken, jsonPath);
            var data = ExecuteRecord(endpoint);

            return data;
        }

        public IRealtimeDatabaseRecord ExecuteRecord(string apiEndpoint)
        {
            IRealtimeDatabaseRecord data = null;

            try
            {
                var response = _httpClient.GetAsync(apiEndpoint).Result;

                if (Equals(response.StatusCode, HttpStatusCode.Unauthorized))
                    throw new UnauthorizedAccessException(response.ReasonPhrase);

                var firebaseData = response.Content.ReadAsStringAsync();
                
                var test = JsonSerializer.Deserialize<IRealtimeDatabaseRecord>(firebaseData.Result);

                return data;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // private bool VerifyCustomToken(string customToken, string apiKey)
        // {
        //     var isTokenVerified = false;

        //     try
        //     {
        //         _httpClient.PostAsync();
        //     }
        // }
    }
}