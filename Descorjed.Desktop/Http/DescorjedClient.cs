using Descorjed.Client.Http.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Descorjed.Client.Http
{
    class DescorjedClient
    {
        public string Token { get; private set; }

        public HttpClient HttpClient { get; set; }


        public string Address { get; set; }

        public DescorjedClient(string token = null)
        {
            this.Token = token;

            this.Address = "http://95.216.203.80:45000/";

            this.HttpClient = new HttpClient();

            if (!String.IsNullOrEmpty(this.Token))
            {
                this.HttpClient.DefaultRequestHeaders.Add("Authorization", this.Token);
            }
        }

        
        public async Task<List<Server>> GetServers()
        {

            var servers = new List<Server>();
            servers = JsonConvert.DeserializeObject<List<Server>>(await DoRequest("api/Servers", HttpMethod.Get));

            return servers;
        }


        public async Task<String> DoRequest(string endpoint, HttpMethod method, Dictionary<String, String> query = null)
        {

            if (this.HttpClient is null || endpoint is null || method == null) throw new NullReferenceException("Endpoint, client or method is null");

            var response = await this.HttpClient.SendAsync(new HttpRequestMessage
            {
                RequestUri = new Uri($"{this.Address}{endpoint}"),
                Method = method
            });
            
            
            return await response.Content.ReadAsStringAsync();
        }
    }


    class ApiException : Exception { 

        public ApiException()
        {
        }

        public ApiException(string message) : base(message)
        {
        }


    }
}

