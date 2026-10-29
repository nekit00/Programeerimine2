using System.Collections.Generic;

namespace KooliProjekt.Application.Dto
{
    // Ühe kategooria vaade: kategooria + selle tooted
    public class ClassDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
