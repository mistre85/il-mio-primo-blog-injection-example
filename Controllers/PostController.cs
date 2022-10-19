using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetCore.Models;
using NetCore_01.Data;
using NetCore_01.Models;
using NetCore_01.Models.Repositories;
using NetCore_01.Models.Repositories.Interfaces;
using System.Diagnostics;

namespace NetCore_01.Controllers
{
   
    public class PostController : Controller
    {
        private IPostRepository postRepository;

        public PostController(IPostRepository _postRepository)
        {
            this.postRepository = _postRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Post> posts = postRepository.GetList();

            return View("Index", posts);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            // aggiungiamo l'include di Tags
            Post postFound = postRepository.GetById(id);

            if (postFound == null)
            {
                return NotFound($"Il post con id {id} non è stato trovato");
            }
            else
            {
                return View("Details", postFound);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            using (BlogContext context = new BlogContext())
            {
                List<Category> categories = context.Categories.ToList();
                List<Tag> tags = context.Tags.ToList();

                PostCategories model = new PostCategories();
                model.Post = new Post();
                model.Categories = categories;

                List<SelectListItem> listTags = new List<SelectListItem>();

                foreach(Tag tag in tags)
                {
                    listTags.Add(new SelectListItem() { Text = tag.Title, Value = tag.Id.ToString() });
                }

                model.Tags = listTags;

                return View("Create", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PostCategories data)
        {
            if (!ModelState.IsValid)
            {
                using (BlogContext context = new BlogContext())
                {
                    // In caso di errore di validazione dobbiamo restituire ogni volta il model
                    // popolato con la lista delle categorie, perchè questi dati non vengono
                    // passati dal post e restituendolo direttamente la property Categories
                    // sarebbe null e la pagina darebbe errore
                    List<Category> categories = context.Categories.ToList();
                    List<Tag> tags = context.Tags.ToList();

                    data.Categories = categories;

                    List<SelectListItem> listTags = new List<SelectListItem>();

                    foreach (Tag tag in tags)
                    {
                        listTags.Add(new SelectListItem() { Text = tag.Title, Value = tag.Id.ToString() });
                    }

                    data.Tags = listTags;

                    return View("Create", data);
                }
            }

            Post postToCreate = new Post();
            postToCreate.Title = data.Post.Title;
            postToCreate.Description = data.Post.Description;
            postToCreate.Image = data.Post.Image;
            postToCreate.CategoryId = data.Post.CategoryId;

            postRepository.Create(postToCreate, data.SelectedTags);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            using (BlogContext context = new BlogContext())
            {
                // aggiungiamo include perchè vogliamo caricare anche i Tag del Post
                Post postToEdit = postRepository.GetById(id);

                if (postToEdit == null)
                {
                    return NotFound();
                }
                else
                {
                    List<Category> categories = context.Categories.ToList();

                    PostCategories model = new PostCategories();
                    model.Post = postToEdit;
                    model.Categories = categories;

                    List<Tag> tags = context.Tags.ToList();

                    List<SelectListItem> listTags = new List<SelectListItem>();

                    foreach (Tag tag in tags)
                    {
                        listTags.Add(
                            new SelectListItem() { 
                                Text = tag.Title, 
                                Value = tag.Id.ToString(), 
                                // dobbiamo settare come selezionati i tag che sono presenti nel Post
                                Selected = postToEdit.Tags.Any(m=>m.Id == tag.Id) 
                            });
                    }

                    model.Tags = listTags;

                    return View(model);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PostCategories data)
        {
            if (!ModelState.IsValid)
            {
                using (BlogContext context = new BlogContext())
                {
                    // In caso di errore di validazione dobbiamo restituire ogni volta il model
                    // popolato con la lista delle categorie, perchè questi dati non vengono
                    // passati dal post e restituendolo direttamente la property Categories
                    // sarebbe null e la pagina darebbe errore
                    List<Category> categories = context.Categories.ToList();
                    data.Categories = categories;

                    List<Tag> tags = context.Tags.ToList();

                    List<SelectListItem> listTags = new List<SelectListItem>();

                    foreach (Tag tag in tags)
                    {
                        listTags.Add(new SelectListItem() { Text = tag.Title, Value = tag.Id.ToString() });
                    }

                    data.Tags = listTags;

                    return View("Update", data);
                }
            }

            // dobbiamo caricare anche i Tag collegati al post
            Post postToEdit = postRepository.GetById(id);

            if (postToEdit != null)
            {
                // aggiorniamo i campi con i nuovi valori
                postToEdit.Title = data.Post.Title;
                postToEdit.Description = data.Post.Description;
                postToEdit.Image = data.Post.Image;
                postToEdit.CategoryId = data.Post.CategoryId;

                postRepository.Update(postToEdit, data.SelectedTags);

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Post postToDelete = postRepository.GetById(id);

            if (postToDelete != null)
            {
                postRepository.Delete(postToDelete);

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}