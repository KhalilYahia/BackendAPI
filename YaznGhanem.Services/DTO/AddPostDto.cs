namespace Services.DTO
{
    public class AddPostDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { set; get; }
    }
}
