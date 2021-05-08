using System;
using System.Collections.Generic;
using GbWebApp.Domain.ViewModels;

namespace GbWebApp.Domain.DTO
{
    /// <summary> order model </summary>
    public class OrderDTO
    {
        /// <summary> order id </summary>
        public int Id { get; set; }
        /// <summary> name of the customer </summary>
        public string Name { get; set; }
        /// <summary> customer's phone </summary>
        public string Phone { get; set; }
        /// <summary> shipping address </summary>
        public string Address { get; set; }
        /// <summary> order's creation date </summary>
        public DateTime Date { get; set; }
        /// <summary> order contents - collection order items </summary>
        public IEnumerable<OrderItemDTO> Items { get; set; }
    }

    /// <summary> order item model </summary>
    public class OrderItemDTO
    {
        /// <summary> item id </summary>
        public int Id { get; set; }
        /// <summary> id of the product </summary>
        public int ProductId { get; set; }
        /// <summary> price of the product </summary>
        public decimal Price { get; set; }
        /// <summary> quantity  of this product's pieces </summary>
        public int Quantity { get; set; }
    }

    /// <summary> auxiliary container </summary>
    public class OrderModel
    {
        /// <summary> order view model </summary>
        public OrderViewModel Order { get; set; }
        /// <summary> collection of order items </summary>
        public IList<OrderItemDTO> Items { get; set; }
    }
}
