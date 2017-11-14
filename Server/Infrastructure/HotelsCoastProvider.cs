using Server.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Server.Infrastructure
{
    public class HotelsCoastProvider : IHotelsProvider
    {
        private readonly HttpClient _httpClient;

        public HotelsCoastProvider()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:51468")
            };
        }

        public IEnumerable<Hotel> GetHotels()
        {
            var response = _httpClient.GetAsync("api/hotelscoast").Result;
            var content = response.Content
                .ReadAsAsync<IEnumerable<Hotel>>().Result;

            return content;
        }

        public async Task<IEnumerable<Hotel>> GetHotelsAsync()
        {
            var response = await _httpClient.GetAsync("api/hotelscoast").ConfigureAwait(false);
            var content = await response.Content
                .ReadAsAsync<IEnumerable<Hotel>>().ConfigureAwait(false);

            return content;
        }
    }
}