using System.Linq;
using GbWebApp.Domain.DTO;
using GbWebApp.Domain.Entities;

namespace GbWebApp.Services.Mappers
{
    public static class OrderMapper
    {
        public static OrderItemDTO ToDTO(this OrderItem item) => item is null ? null : new OrderItemDTO
        {
            Id = item.Id,
            ProductId = item.ProductId,//?.Id ?? item.ProductId,
            Price = item.Price,
            Quantity = item.Quantity,
        };

        public static OrderItem FromDTO(this OrderItemDTO item) => item is null ? null : new OrderItem
        {
            Id = item.Id,
            Product = new Product { Id = item.ProductId },
            //ProdId = item.ProductId,
            Price = item.Price,
            Quantity = item.Quantity,
        };

        public static OrderDTO ToDTO(this Order order) => order is null ? null : new OrderDTO
        {
            Id = order.Id,
            Name = order.Name,
            Address = order.Address,
            Phone = order.Phone,
            Date = order.Date,
            Items = order.Items.Select(ToDTO)
        };

        public static Order FromDTO(this OrderDTO order) => order is null ? null : new Order
        {
            Id = order.Id,
            Name = order.Name,
            Address = order.Address,
            Phone = order.Phone,
            Date = order.Date,
            Items = order.Items.Select(FromDTO).ToList()
        };
    }
}
