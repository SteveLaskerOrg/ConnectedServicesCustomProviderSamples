/********************************************************
*                                                       *
*   Copyright (C) Microsoft. All rights reserved.       *
*                                                       *
********************************************************/
using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Windows.Media;

namespace BitServiceSMX
{
    public class Constants
    {
        public const string BitServiceRoot = "http://localhost:37241";
    }
    public class BitServiceSMXInstance : IConnectedServiceInstance
    {
        
        private string instanceId;
        private Dictionary<string, object> metadata;
        private string name;
        private HttpClient client;

        // This key provides access to the specific instance (e.g. a single provisioned bit).
        private string accessKey;

        // This constructor creates a new instance with a randomly generated name.
        public BitServiceSMXInstance(string bearerToken)
        {
            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new System.Uri(Constants.BitServiceRoot);
            }
            Random r = new Random();
            name = descriptiveWords[r.Next(descriptiveWords.Length)] + animals[r.Next(animals.Length)];
            string provisionUrl = String.Format("api/bits?name={0}", name);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, provisionUrl);
            request.Headers.Add("Authorization", "Bearer " + bearerToken);
            var result = client.SendAsync(request);
            result.Wait();
            HttpResponseMessage response = result.Result;
            accessKey = "";
            if (response.IsSuccessStatusCode)
            {
                Task<string> task = response.Content.ReadAsStringAsync();
                task.Wait();
                accessKey = task.Result;
                // Strip off unnecessary quotation marks.
                // If the id string has quotes around it, strip them off.
                if (accessKey.StartsWith("\""))
                {
                    accessKey = accessKey.Substring(1, accessKey.Length - 2);
                }
                instanceId = name;
                metadata = new Dictionary<string, object>() {
                    {"url", Constants.BitServiceRoot + "/api/bits/" + name}, 
                    {"dateCreated", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()},
                    {"accessKey", accessKey }
                };
            }
        }

        // This constructor instantiates an instance from a service that already exists.
        public BitServiceSMXInstance(string bearerToken, string nameIn, string accessKeyIn)
        {
            name = nameIn;
            accessKey = accessKeyIn;
            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new System.Uri(Constants.BitServiceRoot);
            }
            string bitServiceUrl = String.Format("api/bits/{0}", name);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, bitServiceUrl);
            request.Headers.Add("Authorization", "Bearer " + bearerToken);
            request.Headers.Add("AccessKey", accessKey);
            var result = client.SendAsync(request);
            result.Wait();
            HttpResponseMessage response = result.Result;
            if (response.IsSuccessStatusCode)
            {
                instanceId = name;
                metadata = new Dictionary<string, object>() {
                    {"url", Constants.BitServiceRoot + "/api/bits/" + name}, 
                    {"dateCreated", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()},
                    {"accessKey", accessKey }
                };
            }
        }

        public string InstanceId { get { return instanceId; } }
        public IReadOnlyDictionary<string, object> Metadata { get { return metadata; } }
        public string Name { get { return name; } }
        public string ProviderId { get { return "ContosoCloud.BitService"; } }

        #region Other stuff
        static private string[] animals = { "Ant", "Antelope", "Ape", "Badger", "Bat", "Bear", "Beaver", "Bird", "Boar", "Camel", "Canary", "Cat", "Cheetah", "Chicken", "Chimpanzee", "Chipmunk", "Cow", "Crab", "Crocodile", "Deer", "Dog", "Dolphin", "Donkey", "Duck", "Eagle", "Elephant", "Ferret", "Fish", "Fox", "Frog", "Goat", "Hamster", "Hare", "Horse", "Kangaroo", "Leopard", "Lion", "Lizard", "Mole", "Monkey", "Mousedeer", "Mule", "Ostritch", "Otter", "Panda", "Parrot", "Pig", "Polecat", "Porcupine", "Rabbit", "Rat", "Rhinoceros", "Seal", "Sheep", "Snake", "Squirrel", "Tapir", "Toad", "Tiger", "Tortoise", "Walrus", "Whale", "Wolf", "Zebra" };
        static private string[] descriptiveWords = { "active", "bad", "big", "bold", "boring", "brave", "bright", "calm", "cautious", "clean", "clever", "cold", "colorful", "complete", "complex", "cruel", "dark", "deep", "distinct", "eager", "evil", "far", "fair", "fast", "false", "fierce", "fine", "fit", "flat", "foolish", "full", "good", "hard", "high", "hot", "hollow", "harmonious", "interesting", "kind", "lazy", "light", "long", "loud", "low", "near", "neat", "negative", "new", "normal", "odd", "old", "passive", "plain", "poor", "positive", "powerful", "precise", "primitive", "proud", "pure", "quick", "quiet", "real", "rich", "rotten", "rough", "sad", "sharp", "short", "sick", "silent", "simple", "skillful", "slow", "small", "smooth", "solid", "soft", "stable", "strong", "sweet", "symmetrical", "tangible", "true", "valuable", "weak", "wild", "young" };
        #endregion
    }

    [Export(typeof(IConnectedServiceProvider))]
    [ExportMetadata("ProviderId", "ContosoCloud.BitService")]
    public class BitServiceSMXProvider : IConnectedServiceProvider, IConnectedServiceProviderGridUI, IConnectedServiceHyperlinkAuthenticator, IConnectedServiceInstanceCreator, INotifyPropertyChanged
    {
        #region Description
        public string Category { get { return "ContosoCloud"; } }
        public string Name { get { return "BitService"; } }



        #endregion

        #region Authentication

        private bool isAuthenticated = false;
        private string username;
        private string password;
        private string bearerToken;
        private HttpClient client;
        public bool CanChangeAuthentication { get { return true; } }
        public bool IsAuthenticated { get { return isAuthenticated; } }
        public string ChangeAuthenticationText { get { return isAuthenticated ? "Logout" : "Login"; } }
        public string NeedToAuthenticateText { get { return "Please log in"; } }
        public Task<bool> ChangeAuthentication(IDictionary<string, string> configuration, CancellationToken ct)
        {
            if (!isAuthenticated)
            {
                var d = new LoginWindow();
                d.ShowModal();
                username = d.UserName;
                password = d.Password;
                // Replace the following code with your own authentication
                client = new HttpClient();
                client.BaseAddress = new Uri(Constants.BitServiceRoot);
                string tokenAddress = "Token";
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, tokenAddress);

                string requestBody = String.Format("grant_type=password&username={0}&password={1}", username, password);
                request.Content = new StringContent(requestBody);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                var result = client.PostAsync(tokenAddress, request.Content);
                result.Wait();
                HttpResponseMessage response = result.Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseBodyTask = response.Content.ReadAsAsync<JObject>();
                    responseBodyTask.Wait();
                    var responseBody = responseBodyTask.Result;
                    JToken value;
                    responseBody.TryGetValue("access_token", out value);
                    bearerToken = value.ToString();
                }
                else
                {
                    string errorMessage = String.Format("Unexpected HTTP response. Status code: {0} ", response.StatusCode);
                    throw new Exception(errorMessage);
                }
            }
            else
            {
                username = null;
            }

            isAuthenticated = !isAuthenticated;
            FirePropertyChange("IsAuthenticated");
            FirePropertyChange("CanCreateServiceInstance");
            FirePropertyChange("AuthenticatedText");
            FirePropertyChange("ChangeAuthenticationText");
            return Task.FromResult(true);
        }
        public string AuthenticatedText { get { return isAuthenticated ? username : ""; } }

        #endregion Authentication

        #region Instances
        private List<BitServiceSMXInstance> instances = new List<BitServiceSMXInstance>();

        public async Task<IEnumerable<IConnectedServiceInstance>>
            EnumerateServiceInstancesAsync(IDictionary<string, string> configuration, CancellationToken ct)
        {
            List<IConnectedServiceInstance> returnList = new List<IConnectedServiceInstance>();
            string address = String.Format("api/bits");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, address);
            request.Headers.Add("Authorization", "Bearer " + bearerToken);
            HttpResponseMessage response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                // TODO: report error
                return (IEnumerable<IConnectedServiceInstance>) returnList;
            }

            JObject results = await response.Content.ReadAsAsync<JObject>();
            
            foreach (System.Collections.Generic.KeyValuePair<string, JToken> pair in results)
            {
                /*IJEnumerable<JToken> values = item.Values();
                IEnumerator<JToken> enumerator = values.GetEnumerator();
                enumerator.Reset();
                enumerator.MoveNext();
                string name = enumerator.Current.ToString();
                enumerator.MoveNext();*/
                string name = pair.Key;
                JToken token = pair.Value;
                string accessKey = token.ToString();
                BitServiceSMXInstance newInstance = new BitServiceSMXInstance(bearerToken, name, accessKey);
                returnList.Add(newInstance);

            }
            return (IEnumerable<IConnectedServiceInstance>) returnList;

        }
        public string EnumeratingServiceInstancesText { get { return "Retrieving instances"; } }
        public IEnumerable<System.Tuple<string, string>> ColumnMetadata
        {
            get
            {
                return new List<Tuple<string, string>> {
                    Tuple.Create("url", "Url"),
                    Tuple.Create("accessKey", "Key")
                };
            }
        }
        public IEnumerable<Tuple<string, string>> DetailMetadata
        {
            get
            {
                return new List<Tuple<string, string>> {
                    Tuple.Create("url", "Url"),
                    Tuple.Create("dateCreated", "Date Created")
                };
            }
        }
        public string ServiceInstanceNameLabelText
        {
            get { return "Instance name"; }
        }

        public string ShortDescription
        {
            get { return this.Description; }
        }

        public string GridHeaderText
        {
            get { return null; }
        }
        #endregion

        #region Create
        public bool CanCreateServiceInstance { get { return isAuthenticated; } }
        public string CreateServiceInstanceText { get { return "Create a random instance"; } }
        public Task<IConnectedServiceInstance> CreateServiceInstance(IDictionary<string, string> configuration, CancellationToken ct)
        {
            BitServiceSMXInstance newInstance = new BitServiceSMXInstance(bearerToken);
            instances.Add(newInstance);
            return Task.FromResult<IConnectedServiceInstance>(newInstance);
        }
        public string NoServiceInstancesText { get { return "No service instances. Please create one!"; } }
        
        #endregion

        #region Other stuff
        public Uri MoreInfoUri { get { return new Uri("http://notused.com"); } }

        public string Description { get { return "Not used"; } }

        public ImageSource Icon
        {
            get
            {
                return null;
            }
        }

        public string CreatedBy
        {
            get
            {
                return "Contoso, Inc.";
            }
        }

        public object CreateService(Type serviceType)
        {
            if (serviceType.IsAssignableFrom(this.GetType()))
            {
                return this;
            }

            return null;
        }

        private void FirePropertyChange(string propertyName)
        {
            var handler = this.PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;



        #endregion
    }

    // Handler for C# Windows Store projects.
    [Export(typeof(IConnectedServiceInstanceHandler))]
    [ExportMetadata("ProviderId", "ContosoCloud.BitService")]
    [ExportMetadata("AppliesTo", "WindowsAppContainer+CSharp")]
    public class BitServiceSMXCSharpWindowsStoreHandler : IConnectedServiceInstanceHandler
    {
        async public Task AddServiceInstanceAsync(
            IConnectedServiceInstanceContext context, CancellationToken ct)
        {
            // Add a file
            string newFunctionTokenReplaced = Path.GetTempFileName();
            using (StreamWriter s = new StreamWriter(newFunctionTokenReplaced))
            {
                await s.WriteAsync(
                    HandlerHelper.PerformTokenReplacement(
                        context,
                        BitServiceSMX.Properties.Resources.NewFunction, null));
                s.Close();
            }

            await HandlerHelper.AddFileAsync(
                context, newFunctionTokenReplaced, "NewFunction.cs", null, openOnCompleted: true);

            // Inject a snippet
            // Note: this only works if the client project is a Windows Store app.
            string snippetTokenReplaced =
                HandlerHelper.PerformTokenReplacement(context,
                    BitServiceSMX.Properties.Resources.AppStartSnippet, null);
            
            //TODO: enable
            //await context.HandlerHelper.InjectCodeAsync(
            //    context, InjectionContext.AppStart, snippetTokenReplaced);
        }
    }

    // Handler for other C# project types.

    [Export(typeof(IConnectedServiceInstanceHandler))]
    [ExportMetadata("ProviderId", "ContosoCloud.BitService")]
    [ExportMetadata("AppliesTo", "!WindowsAppContainer+CSharp")]
    public class BitServiceSMXCSharpHandler : IConnectedServiceInstanceHandler
    {
        async public Task AddServiceInstanceAsync(
            IConnectedServiceInstanceContext context, CancellationToken ct)
        {
            // Add a file
            string newFunctionTokenReplaced = Path.GetTempFileName();
            using (StreamWriter s = new StreamWriter(newFunctionTokenReplaced))
            {
                await s.WriteAsync(
                    HandlerHelper.PerformTokenReplacement(
                        context,
                        BitServiceSMX.Properties.Resources.NewFunction, null));
                s.Close();
            }

            await HandlerHelper.AddFileAsync(
                context, newFunctionTokenReplaced, "NewFunction.cs", null, openOnCompleted: true);

            // Get the appropriate VS Extensibility model types
            // and add a reference to System.Net.Http.
            Microsoft.VisualStudio.Shell.Interop.IVsHierarchy ivs = context.ProjectHierarchy;
            object projectObj;
            ivs.GetProperty(Microsoft.VisualStudio.VSConstants.VSITEMID_ROOT,
                           (int)Microsoft.VisualStudio.Shell.Interop.__VSHPROPID.VSHPROPID_ExtObject,
                           out projectObj);
            EnvDTE.Project project = (EnvDTE.Project)projectObj;
            var vsProject = project.Object as VSLangProj.VSProject;
            vsProject.References.Add(@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.Net.Http.dll");
        }
    }
}