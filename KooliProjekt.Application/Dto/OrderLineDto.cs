namespace KooliProjekt.Application.Dto
{
    // Tellimuse rida: mis toode, mitu tükki, ühiku hind ja rea summa
    public class OrderLineDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductPhoto { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }
}
