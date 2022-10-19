using NetCore.Models;

namespace NetCore_01.Models.Repositories.Interfaces
{
    public interface IPostRepository
    {
        public List<Post> GetList();

        public List<Post> GetListByFilter(string search);

        public Post GetById(int id);

        public void Create(Post post, List<string> selectedTags);

        public void Update(Post post, List<string> selectedTags);

        public void Delete(Post post);
    }
}
