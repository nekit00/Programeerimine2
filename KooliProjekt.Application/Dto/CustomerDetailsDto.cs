using System.Collections.Generic;

namespace KooliProjekt.Application.Dto
{
    // Kliendi vaade: kontaktandmed + tema tellimuste ajalugu
    public class CustomerDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int OrderCount { get; set; }
        public decimal TotalSpent { get; set; }
        public IList<OrderDto> Orders { get; set; } = new List<OrderDto>();
    }
}
