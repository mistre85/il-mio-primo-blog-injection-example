using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetCore.Models;
using NetCore_01.Data;
using NetCore_01.Models.Repositories.Interfaces;

namespace NetCore_01.Models.Repositories
{
    
    public class DbPostRepository : IPostRepository
    {
        BlogContext context;
        public DbPostRepository(BlogContext ctx)
        {
            context = ctx;
        }

        public void Create(Post post, List<string> selectedTags)
        {
            using (BlogContext context = new BlogContext())
            {
                post.Tags = new List<Tag>();

                if (selectedTags != null)
                {
                    foreach (string selectedTagId in selectedTags)
                    {
                        int selectedIntTagId = int.Parse(selectedTagId);

                        Tag tag = context.Tags.Where(m => m.Id == selectedIntTagId).FirstOrDefault();

                        post.Tags.Add(tag);
                    }
                }

                context.Posts.Add(post);

                context.SaveChanges();
            }
        }

        public void Delete(Post post)
        {
            using (BlogContext context = new BlogContext())
            {
                context.Posts.Remove(post);

                context.SaveChanges();
            }
        }

        public Post GetById(int id)
        {
            using (BlogContext context = new BlogContext())
            {
                Post postFound = context.Posts.Where(post => post.Id == id).Include(post => post.Category).Include(m => m.Tags).FirstOrDefault();

                return postFound;
            }
        }

        public List<Post> GetList()
        {
            using (BlogContext context = new BlogContext())
            {
                IQueryable<Post> posts = context.Posts;

                return posts.ToList();
            }
        }

        public List<Post> GetListByFilter(string search)
        {
            using (BlogContext context = new BlogContext())
            {
                IQueryable<Post> posts = context.Posts;

                if (search != null)
                {
                    posts = posts.Where(post => post.Title.ToLower().Contains(search.ToLower()));
                }

                return posts.ToList();
            }
        }

        public void Update(Post post, List<string> selectedTags)
        {
            using (BlogContext context = new BlogContext())
            {
                // dobbiamo fare l'attach perchè altrimenti ci sono problemi
                // nel salvataggio dei tag
                // facendo però l'attach è necessario prima aggiornare anche l'entity
                // della categoria, avere l'id aggiornato non basta.
                // l'attach infatti sovrascrive il nuovo valore di id categoria
                // con quello precedente

                post.Category = context.Categories.Where(m => m.Id == post.CategoryId).FirstOrDefault();

                context.Attach(post);

                // rimuoviamo i tag e inseriamo i nuovi
                post.Tags.Clear();

                if (selectedTags != null)
                {
                    foreach (string selectedTagId in selectedTags)
                    {
                        int selectedIntTagId = int.Parse(selectedTagId);

                        Tag tag = context.Tags.Where(m => m.Id == selectedIntTagId).FirstOrDefault();

                        post.Tags.Add(tag);
                    }
                }

                context.Update(post);

                context.SaveChanges();
            }
        }
    }
}
