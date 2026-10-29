namespace KooliProjekt.Application.Dto
{
    // Kategooria nimekirjas: nimi + mitu toodet selles kategoorias on
    public class ClassDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductCount { get; set; }
    }
}
