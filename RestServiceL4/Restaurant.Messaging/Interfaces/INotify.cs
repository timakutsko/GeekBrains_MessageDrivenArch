using Restaurant.Messages.Interfaces.Entitis;

namespace Restaurant.Messages.Interfaces
{
    public interface INotify : IClient
    {
        public string Message { get; }
    }
}
