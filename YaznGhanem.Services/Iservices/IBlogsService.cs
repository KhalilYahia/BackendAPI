
using Services.DTO;

namespace Services.Iservices
{
    public interface IBlogsService
    {
        Task<int> AddNewBlog(AddBlogDto dto);
        List<GetBlogDto> GetAllBlogs();
    }
}
