namespace KooliProjekt.Application.Dto
{
    // Toode nimekirjas (nagu e-poe tootekaart): pilt, nimi, hind, kategooria
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public decimal Price { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
    }
}
