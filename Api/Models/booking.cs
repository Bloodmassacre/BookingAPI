using Microsoft.Extensions.Logging;
namespace Api.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int event_id { get; set; }
        public int user_id { get; set; }
        public DateTime create_at { get; set; } = DateTime.Now;

    }
}
