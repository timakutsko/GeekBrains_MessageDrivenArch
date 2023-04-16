using Restaurant.Messages.Interfaces.Entitis;

namespace Restaurant.Messages.Interfaces
{
    public interface ITableBooked : IClient, IDish
    {
        public int TableId { get; }
    }
}
