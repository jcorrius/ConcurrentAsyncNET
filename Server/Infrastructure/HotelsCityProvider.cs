using Server.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Server.Infrastructure
{
    public class HotelsCityProvider : IHotelsProvider
    {
        private readonly HttpClient _httpClient;

        public HotelsCityProvider()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:51468")
            };
        }

        public IEnumerable<Hotel> GetHotels()
        {
            var response = _httpClient.GetAsync("api/hotelscity").Result;
            var content = response.Content
                .ReadAsAsync<IEnumerable<Hotel>>().Result;

            return content;
        }

        public async Task<IEnumerable<Hotel>> GetHotelsAsync()
        {
            var response = await _httpClient.GetAsync("api/hotelscity").ConfigureAwait(false);
            var content = await response.Content
                .ReadAsAsync<IEnumerable<Hotel>>().ConfigureAwait(false);

            return content;
        }
    }
}