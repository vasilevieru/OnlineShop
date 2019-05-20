using OnlineShop.Domain.Enumerations;
using OnlineShop.Domain.Extensions;

namespace OnlineShop.Domain.Entities
{
    public class OrderStatus : BaseEntity
    {
        public string Description { get; set; }

        public OrderStatus()
        {
        }

        public OrderStatus(OrderStatusEnum enumType)
        {
            Id = (int)(object)enumType;
            Description = enumType.GetEnumDescription();
        }

        public static implicit operator OrderStatus(OrderStatusEnum enumType) => new OrderStatus(enumType);

        public static implicit operator OrderStatusEnum(OrderStatus enumType) =>
            (OrderStatusEnum)(object)enumType.Id;
    }
}
