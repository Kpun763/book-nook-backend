namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class ReviewWithUserDTO
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Text { get; set; }
        public double Rating { get; set; }
        public UserForDisplayDto Username { get; set; }


    }
}
