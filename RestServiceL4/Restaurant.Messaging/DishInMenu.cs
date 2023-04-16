using System;

namespace Restaurant.Messages
{
    public interface IDishReady
    {
        public Guid OrderId { get; }

        public Dish? Dish { get; }

        public bool InMenu { get; }
    }


    public class DishInMenu : IDishReady
    {
        public DishInMenu(Guid orderId, Dish? dish, bool inMenu)
        {
            OrderId = orderId;
            Dish = dish;
            InMenu = inMenu;
        }
        public Guid OrderId { get; }

        public Dish? Dish { get; }

        public bool InMenu { get; }
    }
}
