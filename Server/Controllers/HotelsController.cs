using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Server.Models;

namespace Server.Controllers
{
    public class HotelsController : ApiController
    {
        private readonly IEnumerable<IHotelsProvider> _hotelProviders;

        public HotelsController(IEnumerable<IHotelsProvider> hotelProviders)
        {
            _hotelProviders = hotelProviders;
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
            var hotels = _hotelProviders.SelectMany(p => p.GetHotels());
            return Ok(hotels);
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
            var hotels = _hotelProviders.AsParallel()
                .SelectMany(p => p.GetHotels()).AsEnumerable();                
            return Ok(hotels);
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
            foreach (var provider in _hotelProviders)
            {

                var hotels = await provider.GetHotelsAsync().ConfigureAwait(false);
                hotelsResult.AddRange(hotels);
            }
            
            return Ok(hotelsResult);
        }

        //
        // ---------------------------------------------------
        // Asynchronous and In Parallel(In a Blocking Fashion)
        // --------------------------------------------------
        //
        [Route("api/hotels/AllHotelsInParallelBlockingAsync")]
        [HttpGet]
        public IHttpActionResult AllHotelsInParallelBlockingAsync()
        {
            var allTasks =_hotelProviders.Select(p => p.GetHotelsAsync());
            Task.WaitAll(allTasks.ToArray());

            return Ok(allTasks.SelectMany(task => task.Result));
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
            var allTasks = _hotelProviders.Select(p => p.GetHotelsAsync());
            var allResults = await Task.WhenAll(allTasks).ConfigureAwait(false);
            return Ok(allResults.SelectMany(hotels => hotels));
        }
    }
}
