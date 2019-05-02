namespace BookStoreApp.Models.DTO
{
    public static class AuthorDTOMapper
    {
        public static AuthorDTO MapToDTO(Author author)
        {
            return new AuthorDTO()
            {
                Id = author.Id,
                Name = author.Name,

                AuthorContact = new AuthorContactDTO()
                {
                    AuthorId = author.Id,
                    Address = author.AuthorContact.Address,
                    ContactNumber = author.AuthorContact.ContactNumber
                }
            };
        }
    }
}
