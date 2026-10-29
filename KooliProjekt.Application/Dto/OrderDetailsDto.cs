using System;
using System.Collections.Generic;

namespace KooliProjekt.Application.Dto
{
    // Tellimuse vaade (nagu arve): päis, kliendi andmed, read ja kogusumma
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public IList<OrderLineDto> Lines { get; set; } = new List<OrderLineDto>();
        public int ItemCount { get; set; }
        public decimal Total { get; set; }
    }
}
