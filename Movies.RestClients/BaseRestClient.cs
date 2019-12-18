using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.RestClients
{
    public class BaseRestClient
    {
        public RestClient RestClient { get; set; }

        public BaseRestClient(string baseUrl)
        {
            RestClient = new RestClient(baseUrl);
        }

        public BaseRestClient()
        {
            RestClient = new RestClient();
        }
    }
}
