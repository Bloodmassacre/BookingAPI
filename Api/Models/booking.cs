using Microsoft.Extensions.Logging;
namespace Api.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int event_id { get; set; }
        public string user_id { get; set; }
        public DateTime createDate { get; set; } = DateTime.Now;

    }
}
