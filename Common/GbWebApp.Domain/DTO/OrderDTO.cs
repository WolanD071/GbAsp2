using System;
using System.Collections.Generic;
using GbWebApp.Domain.ViewModels;

namespace GbWebApp.Domain.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<OrderItemDTO> Items { get; set; }
    }

    public class OrderItemDTO
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderModel
    {
        public OrderViewModel Order { get; set; }
        public IList<OrderItemDTO> Items { get; set; }
    }
}
