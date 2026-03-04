using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class BookingRequest
    {
        public int event_id { get; set; }
        public string user_id { get; set; }
    }
}
