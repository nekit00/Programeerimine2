using System;

namespace KooliProjekt.Application.Dto
{
    // Tellimus nimekirjas: kuupäev, staatus, klient, mitu toodet ja kogusumma
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int ItemCount { get; set; }
        public decimal Total { get; set; }
    }
}
