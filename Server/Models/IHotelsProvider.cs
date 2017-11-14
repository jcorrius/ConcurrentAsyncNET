using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Models
{
    public interface IHotelsProvider
    {
        IEnumerable<Hotel> GetHotels();

        Task<IEnumerable<Hotel>> GetHotelsAsync();
    }
}