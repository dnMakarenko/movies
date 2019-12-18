using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;
using Microsoft.Extensions.Options;
using Movies.Dtos.Rest;
using System.Runtime.Serialization;

namespace Movies.RestClients
{
    public class MoviesRestClient : BaseRestClient, IMoviesRestClient
    {
        private readonly string _apiKey;

        public MoviesRestClient(IOptions<OmDbApiCfg> cfg):base(cfg.Value.BaseOmDbApiUrl) 
        {
            _apiKey = cfg.Value.ApiKey;
        }

        /// <summary>
        /// Returns movie from remote omdbapi server by movie name
        /// </summary>
        /// <param name="title">Represents the name of the movie</param>
        /// <returns>MovieDto that represents movie</returns>
        public async Task<MovieDto> GetMovieAsync(string title)
        {
            try
            {
                var request = new RestRequest("?", Method.GET) { RequestFormat = DataFormat.Json };
                request.AddParameter("apikey", _apiKey);
                request.AddParameter("t", title);

                RestResponse resp = RestClient.Execute(request) as RestResponse;

                if (resp != null)
                {
                    if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        JToken responseContent = JObject.Parse(resp.Content);
                        var responseDto = responseContent.ToObject<MovieDto>();

                        return responseDto;
                    }
                }

                throw new HttpRequestException($"Couldn't return movie from {base.RestClient.BaseUrl}. StatusCode:'{resp.StatusCode}'. ErrorMessage:'{resp.ErrorMessage}'");
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException($"Couldn't return movie from {base.RestClient.BaseUrl}. ErrorMessage:'{e.Message}'");
            }
            catch (SerializationException e)
            {
                throw new SerializationException($"Couldn't return movie from {base.RestClient.BaseUrl}. ErrorMessage:'{e.Message}'");
            }
            catch (Exception e)
            {
                throw new Exception($"Couldn't return movie from {base.RestClient.BaseUrl}. ErrorMessage:'{e.Message}'");
            }
        }
    }
}
