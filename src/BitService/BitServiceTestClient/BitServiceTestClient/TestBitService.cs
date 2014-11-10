using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Linq;


namespace BitServiceTestClient
{

    class TestBitService
    {

        const int FAILURE = -1;
        const int SUCCESS = 1;

        HttpClient client;

        // Hash provided by ASP.NET for authentication
        string bearerToken;


        bool bitValue;

        TestBitService(string baseAddress)
        {
            client = new HttpClient();
            client.BaseAddress = new System.Uri(baseAddress);
        }

        // Step 0: create a login identity on the service
        int Register(string username, string password)
        {

            //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            
            string requestLoginAddress = String.Format("api/Account/Register");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestLoginAddress);
            
            string requestBody = String.Format("{{ \"UserName\": \"{0}\", \"Password\": \"{1}\", \"ConfirmPassword\": \"{1}\" }}", username, password);
            request.Content = new StringContent(requestBody);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result1 = client.SendAsync(request);
            result1.Wait();
            HttpResponseMessage response = result1.Result;
            if (!response.IsSuccessStatusCode)
            {
                return FAILURE;
            }

            Task<string> task1 = response.Content.ReadAsStringAsync();
            task1.Wait();
            string result2 = task1.Result;
            return SUCCESS;
        }

        int Login(string username, string password)
        {
            string tokenAddress = "Token";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, tokenAddress);
            
            string requestBody = String.Format("grant_type=password&username={0}&password={1}", username, password);
            request.Content = new StringContent(requestBody);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var result = client.PostAsync(tokenAddress, request.Content);
            result.Wait();
            HttpResponseMessage response = result.Result;
            if (!response.IsSuccessStatusCode)
            {
                return FAILURE;
            }

            var responseBodyTask = response.Content.ReadAsAsync<JObject>();
            responseBodyTask.Wait();
            var responseBody = responseBodyTask.Result;
            JToken value;
            responseBody.TryGetValue("access_token", out value);
            bearerToken = value.ToString();

            return SUCCESS;
        }

        int ProvisionBit(string bitName, out string accessKey)
        {

            // Step 2: Provision a bit using the authenticated connection
            string address = String.Format("api/bits?name={0}", bitName);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, address);
            request.Headers.Add("Authorization", "Bearer " + bearerToken);
            var result = client.SendAsync(request);
            result.Wait();
            HttpResponseMessage response = result.Result;
            if (!response.IsSuccessStatusCode)
            {
                accessKey = "";
                return FAILURE;
            }

            Task<string> task1 = response.Content.ReadAsStringAsync();
            task1.Wait();
            accessKey = task1.Result;
            // If the id string has quotes around it, strip them off.
            if (accessKey.StartsWith("\""))
            {
                accessKey = accessKey.Substring(1, accessKey.Length - 2);
            }
            return SUCCESS;
        }

        int ReadBit(string bitName, string accessKey)
        {
            string address = String.Format("api/bits/{0}", bitName);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, address);
            request.Headers.Add("AccessKey", accessKey);
            var result = client.SendAsync(request);
            result.Wait();
            HttpResponseMessage response = result.Result;
            if (!response.IsSuccessStatusCode)
            {
                return FAILURE;
            }

            Task<string> task1 = response.Content.ReadAsStringAsync();
            task1.Wait();
            if (task1.Result.ToLower() == "true")
            {
                bitValue = true;
            }
            else if (task1.Result.ToLower() == "false")
            {
                bitValue = false;
            }
            else
            {
                return FAILURE;
            }

            return SUCCESS;
        }

        int SetBit(string bitName, string accessKey, bool value )
        {
            string address = String.Format("api/bits/{0}?value={1}", bitName, value.ToString().ToLower());
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, address);
            request.Headers.Add("Authorization", "Bearer " + bearerToken);
            request.Headers.Add("AccessKey", accessKey);
            var result = client.SendAsync(request);
            result.Wait();
            HttpResponseMessage response = result.Result;
            if (!response.IsSuccessStatusCode)
            {
                return FAILURE;
            }

            Task<string> task1 = response.Content.ReadAsStringAsync();
            task1.Wait();
            if (task1.Result == "true")
            {
                return SUCCESS;
            }
            else if (task1.Result == "false")
            {
                return SUCCESS;
            }
            else
            {
                return FAILURE;
            }

        }

        int ReleaseBit(string bitName, string accessKey)
        {
            string address = String.Format("api/bits/{0}", bitName);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, address);
            request.Headers.Add("Authorization", "Bearer " + bearerToken);
            request.Headers.Add("AccessKey", accessKey);
            var result = client.SendAsync(request);
            result.Wait();
            HttpResponseMessage response = result.Result;
            if (!response.IsSuccessStatusCode)
            {
                return FAILURE;
            }

            Task<string> task1 = response.Content.ReadAsStringAsync();
            task1.Wait();
            if (task1.Result == "true")
            {
                return SUCCESS;
            }
            else if (task1.Result == "false")
            {
                return FAILURE;
            }
            else
            {
                return FAILURE;
            }
        }


        static void PrintErrorAndExit(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Press any key to terminate.");
            Console.ReadLine();
            Environment.Exit(FAILURE);
        }
        static void Main(string[] args)
        {
            // Enter the service URL here
            string baseAddress = "http://localhost";
            // Enter your port number here
            int port = 37241; 
            string username = "billy";
            string password = "test@123";
            TestBitService prog = new TestBitService(baseAddress + ":" + port.ToString() + "/");
            int error;
#if true
            error = prog.Register(username, password);
            if (error != SUCCESS)
            {
                PrintErrorAndExit("Register failed.");
            }
#endif
            error = prog.Login(username, password);
            if (error != SUCCESS)
            {
                PrintErrorAndExit("Login failed.");
            }
            string bitName = "test123456";
            string accessKey;
            error = prog.ProvisionBit(bitName, out accessKey);
            if (error != SUCCESS)
            {
                PrintErrorAndExit("Provision bit failed.");
                return;
            }

            error = prog.EnumerateBits();

            //string accessKey = "e40af78e-3945-49f4-a454-4d533844a9d8";
            error = prog.ReadBit(bitName, accessKey);
            if (error != SUCCESS)
            {
                PrintErrorAndExit("Reading bit failed 1.");
                return;
            }
            error = prog.SetBit(bitName, accessKey, true);
            if (error != SUCCESS)
            {
                PrintErrorAndExit("Setting bit failed 1.");
                return;
            }
            error = prog.ReadBit(bitName, accessKey);
            if (error != SUCCESS)
            {
                PrintErrorAndExit("Reading bit failed 2.");
                return;
            }
            error = prog.SetBit(bitName, accessKey, false);
            if (error != SUCCESS)
            {
                PrintErrorAndExit("Setting bit failed 2.");
                return;
            }
            error = prog.ReadBit(bitName, accessKey);
            if (error != SUCCESS)
            {
                PrintErrorAndExit("Reading bit failed 3.");
                return;
            }
            error = prog.ReleaseBit(bitName, accessKey);
            if (error != SUCCESS)
            {
                PrintErrorAndExit("Release bit failed.");
                return;
            }

            Console.WriteLine("Press any key...");
            Console.ReadLine();

            // Step 7: Release login account
        }

        private int EnumerateBits()
        {
            string address = String.Format("api/bits");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, address);
            request.Headers.Add("Authorization", "Bearer " + bearerToken);
            var result = client.SendAsync(request);
            result.Wait();
            HttpResponseMessage response = result.Result;
            if (!response.IsSuccessStatusCode)
            {
                return FAILURE;
            }

            Task<JArray> task1 = response.Content.ReadAsAsync<JArray>();
            task1.Wait();
            Console.WriteLine("Detected bits:");
            foreach (var item in task1.Result)
            {
                Console.WriteLine("{0}", item.ToString());
            }
            return SUCCESS;
        }
    }
}
