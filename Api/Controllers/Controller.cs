using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly AppDbContext _db;
    // Получаем базу данных
    public BookingsController(AppDbContext db)
    {
        _db = db;
    }
    [HttpPost("reserve")]
    public async Task<IActionResult> ReserveSeat(BookingRequest request)
    {
        try
        {
            // Проверяем наличие события
            var eventItem = await _db.Events.FindAsync(request.event_id);
            if (eventItem == null)
                return BadRequest("Событие не найдено");

            // Проверяем бронь
            var alreadyBooked = await _db.Bookings
                .AnyAsync(b => b.event_id == request.event_id && b.user_id == request.user_id);

            if (alreadyBooked)
                return BadRequest("Вы уже забронировали это событие");

            // Проверяем места
            var bookedSeats = await _db.Bookings
                .CountAsync(b => b.event_id == request.event_id);

            if (bookedSeats >= eventItem.TotalSeats)
                return BadRequest("Нет свободных мест");

            // Бронируем место
            var booking = new Booking
            {
                event_id = request.event_id,
                user_id = request.user_id,
                createDate = DateTime.Now
            };

            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                message = "Успешно забронировано!",
                bookingId = booking.Id
            });
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserBookings(string userId)
    {
        var bookings = await _db.Bookings
            .Where(b => b.user_id == userId)
            .Select(b => new {b.Id, EventName = b.event_id, b.createDate})
            .ToListAsync();

        return (IActionResult)bookings;
    }
}