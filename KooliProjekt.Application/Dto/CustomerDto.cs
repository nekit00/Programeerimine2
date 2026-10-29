namespace KooliProjekt.Application.Dto
{
    // Klient nimekirjas: nimi, e-post ja tellimuste arv
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int OrderCount { get; set; }
    }
}
