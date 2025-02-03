

namespace Services.DTO
{
    public class GetPostDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { set; get; }
        public GetBlogDto Blog { get; set; }
    }
}
