namespace Restaurant.Messages.Interfaces.Entitis
{
    public interface IDish
    {
        public Dish? PreOrder { get; }

        public bool Success { get; }
    }
}
