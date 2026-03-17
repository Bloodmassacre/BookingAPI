using Api.Models;
using Api.Repositories;
using Microsoft.EntityFrameworkCore;

public class BookingService
{
    private readonly BookingRepository _repository;
    private readonly AppDbContext _context;

    public BookingService(BookingRepository repository, AppDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<Booking> ReserveSeatAsync(int eventId, string userId)
    {
        // Проверяем существование события
        var Exists = await _context.Events.AnyAsync(e => e.Id == eventId);
        if (!Exists)
        {
            throw new Exception("Событие не найдено");
        }

        // Проверяем бронь
        var Booked = await _repository.ExistsForUser(eventId, userId);
        if (Booked)
        {
            throw new Exception("Вы уже забронировали это событие");
        }

        // Проверяем свободные места
        var Count = await _repository.CountByEvent(eventId);
        var eventInfo = await _context.Events.FindAsync(eventId);

        if (Count >= eventInfo.TotalSeats)
        {
            throw new Exception("Нет свободных мест");
        }

        // Создаем бронирование
        var booking = _repository.Create(eventId, userId);
        await _context.SaveChangesAsync();

        return booking;
    }
}