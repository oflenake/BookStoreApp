namespace BookStoreApp.Models.DTO
{
    public class AuthorDTO
    {
        public AuthorDTO()
        {
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public AuthorContactDTO AuthorContact { get; set; }
    }
}
