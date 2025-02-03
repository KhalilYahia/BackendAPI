namespace firstProject.Domain.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

       
        public int BlogId { set; get; }
        public Blog Blog { get; set; }
    }
}
