using Api.Models;
using Microsoft.EntityFrameworkCore;
namespace Api.Repositories
{

    public class BookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        // Подсчёт колличества бронирований
        public async Task<int> CountByEvent(int eventId)
        {
            return await _context.Bookings
                .Where(b => b.event_id == eventId)
                .CountAsync();
        }

        // Проверка бронирования
        public async Task<bool> ExistsForUser(int eventId, string userId)
        {
            var X = await _context.Bookings.AnyAsync(b => b.event_id == eventId && b.user_id == userId);
            return X;
        }

        // Создание нового бронирования
        public Booking Create(int eventId, string userId)
        {
            var booking = new Booking
            {
                event_id = eventId,
                user_id = userId,
                createDate = DateTime.Now
            };

            _context.Bookings.Add(booking);
            return booking;
        }
    }
}
