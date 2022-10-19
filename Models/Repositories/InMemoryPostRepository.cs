using Microsoft.AspNetCore.Mvc.Rendering;
using NetCore.Models;
using NetCore_01.Data;
using NetCore_01.Models.Repositories.Interfaces;

namespace NetCore_01.Models.Repositories
{
    // Questa classe è solo a titolo dimostrativo, diciamo che
    // non importa se tutte le funzionalità non sono implementate
    // alla perfezione
    public class InMemoryPostRepository : IPostRepository
    {
        private static List<Post> Posts = new List<Post>();

        public void Create(Post post, List<string> selectedTags)
        {
            post.Id = Posts.Count;

            // diciamo che per semplicità tag e categorie non le gestiamo
            post.Tags = new List<Tag>();

            InMemoryPostRepository.Posts.Add(post);
        }

        public void Delete(Post post)
        {
            int indicePostDaEliminare = -1;

            for(int i = 0; i < InMemoryPostRepository.Posts.Count; i++)
            {
                Post postToCheck = InMemoryPostRepository.Posts[i];

                if (postToCheck.Id == post.Id)
                {
                    indicePostDaEliminare = i;
                }
            }

            if (indicePostDaEliminare != -1)
            {
                InMemoryPostRepository.Posts.RemoveAt(indicePostDaEliminare);
            }
        }

        public Post GetById(int id)
        {
            Post postDaTrovare = null;

            for (int i = 0; i < InMemoryPostRepository.Posts.Count; i++)
            {
                Post postToCheck = InMemoryPostRepository.Posts[i];

                if (postToCheck.Id == id)
                {
                    postDaTrovare = postToCheck;
                }
            }

            return postDaTrovare;
        }

        public List<Post> GetList()
        {
            return InMemoryPostRepository.Posts;
        }

        public List<Post> GetListByFilter(string search)
        {
                List<Post> posts = InMemoryPostRepository.Posts;

                if (search != null)
                {
                    posts = posts.Where(post => post.Title.ToLower().Contains(search.ToLower())).ToList();
                }

                return posts.ToList();
        }

        public void Update(Post post, List<string> selectedTags)
        {
            int indicePostDaTrovare = -1;

            for (int i = 0; i < InMemoryPostRepository.Posts.Count; i++)
            {
                Post postToCheck = InMemoryPostRepository.Posts[i];

                if (postToCheck.Id == post.Id)
                {
                    indicePostDaTrovare = i;
                }
            }

            if (indicePostDaTrovare != -1)
            {
                InMemoryPostRepository.Posts[indicePostDaTrovare] = post;
            }
        }
    }
}
