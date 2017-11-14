using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Server.Models;

namespace Server.Controllers
{
    public class HotelsController : ApiController
    {
        private static readonly string[] Providers = new[] {
            @"http://localhost:51468/api/hotelscoast",
            @"http://localhost:51468/api/hotelscity"
        };

        private async Task<IEnumerable<Hotel>> GetHotelsAsync(string uri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri).ConfigureAwait(false);
                var content = await response.Content
                    .ReadAsAsync<IEnumerable<Hotel>>().ConfigureAwait(false);

                return content;
            }
        }

        private IEnumerable<Hotel> GetHotels(string uri)
        {
            using (var client = new WebClient())
            {
                string hotelsJson = client.DownloadString(uri);
                var hotels = JsonConvert
                    .DeserializeObject<IEnumerable<Hotel>>(hotelsJson);

                return hotels;
            }
        }

        //
        // -------------------------------
        // Synchronous and not in Parallel
        // -------------------------------
        //
        [Route("api/hotels/AllHotelsSync")]
        [HttpGet]
        public IHttpActionResult AllHotelsSync()
        {
            var hotels = Providers.SelectMany(GetHotels);
            return Json(hotels);
        }

        //
        // ---------------------------
        // Synchronous and in Parallel
        // ---------------------------
        //
        [Route("api/hotels/AllHotelsInParallelSync")]
        [HttpGet]
        public IHttpActionResult AllHotelsInParallelSync()
        {

            var hotels = Providers.AsParallel()
                .SelectMany(GetHotels).AsEnumerable();

            return Json(hotels);
        }

        //
        // --------------------------------
        // Asynchronous and not in Parallel
        // --------------------------------
        //
        [Route("api/hotels/AllHotelsAsync")]
        [HttpGet]
        public async Task<IHttpActionResult> AllHotelsAsync()
        {
            var hotelsResult = new List<Hotel>();
            foreach (var uri in Providers)
            {

                var hotels = await GetHotelsAsync(uri);
                hotelsResult.AddRange(hotels);
            }
            
            return Json(hotelsResult);
        }

        //
        // --------------------------------------------------------
        // Asynchronous and In Parallel (In a Non-Blocking Fashion)
        // --------------------------------------------------------
        //
        [Route("api/hotels/AllHotelsInParallelNonBlockingAsync")]
        [HttpGet]
        public async Task<IHttpActionResult> AllHotelsInParallelNonBlockingAsync()
        {
            var allTasks = Providers.Select(GetHotelsAsync);
            var allResults = await Task.WhenAll(allTasks);
            return Json(allResults.SelectMany(hotels => hotels));
        }
    }
}
