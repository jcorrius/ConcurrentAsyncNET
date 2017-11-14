using System.Threading;
using System.Web.Http;
using Providers.Models;

namespace Providers.Controllers
{
    public class HotelsCityController : ApiController
    {
        public IHttpActionResult Get()
        {
            Thread.Sleep(500); // This API should respond in aprox 500 ms 

            var hotels = new Hotel[]
            {
                new Hotel() { Id = 4, Name = "Hotel Hacendado", City = "Valencia", Price = 18.99f, Stars = 1},
                new Hotel() { Id = 5, Name = "Hotel Logitravel", City = "Benidorm", Price = 50.0f, Stars = 3},
                new Hotel() { Id = 6, Name = "Hotel Transilvania", City = "Budapest", Price = 60.0f, Stars = 4}
            };

            return Json(hotels);
        }
    }
}
