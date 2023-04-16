using System;

namespace Restaurant.Booking
{
    /// <summary>
    /// Варианты статуса занятости
    /// </summary>
    public enum BookingState
    {
        /// <summary>
        /// Стол свободен
        /// </summary>
        Free,
        /// <summary>
        /// Стол занят
        /// </summary>
        Booked
    }

    public class Table
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Статус занятости
        /// </summary>
        public BookingState CurrentState { get; set; }

        /// <summary>
        /// Количество посадочных мест
        /// </summary>
        public int SeatsCount { get; }

        public Table(int id)
        {
            Id = id;
            CurrentState = BookingState.Free;
            Random random = new Random();
            SeatsCount = random.Next(2, 5);
        }

        /// <summary>
        /// Занять стол
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool SetState(BookingState state)
        {
            if (state == CurrentState)
                return false;

            CurrentState = state;
            return true;
        }
    }
}
