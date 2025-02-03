using Services.DTO;

namespace firstProject.Iservices
{
    public interface IPostsService
    {
        int AddNewPost(AddPostDto dto);
        List<GetPostDto> GetAllPosts();
    }
}
