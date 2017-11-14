using System.Threading;
using System.Web.Http;
using Providers.Models;

namespace Providers.Controllers
{
    public class HotelsCoastController : ApiController
    {
        public IHttpActionResult Get()
        {
            Thread.Sleep(500); // This API should respond in aprox 500 ms 

            var hotels = new Hotel[]
            {
                new Hotel() { Id = 0, Name = "Hotel California", City = "San Francisco", Price = 150.0f, Stars = 5}, 
                new Hotel() { Id = 1, Name = "Hotel Overlook", City = "Estes Park", Price = 50.0f, Stars = 3},
                new Hotel() { Id = 2, Name = "Hotel Chelsea", City = "New York", Price = 89.2f, Stars = 2}
            };

            return Json(hotels);
        }
    }
}
