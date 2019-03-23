using OnlineShop.Core.Enums;
using OnlineShop.Core.Extensions;

namespace OnlineShop.Core.Domain
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
