namespace BookStoreApp.Models.DTO
{
    public class AuthorContactDTO
    {
        public AuthorContactDTO()
        {
        }

        public long AuthorId { get; set; }

        public string ContactNumber { get; set; }

        public string Address { get; set; }
    }
}
