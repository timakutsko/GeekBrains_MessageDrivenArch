using System;

namespace Restaurant.Messages.Interfaces.Entitis
{
    public interface IClient
    {
        public Guid ClientId { get; }

        public Guid OrderId { get; }
    }
}
