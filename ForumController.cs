using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LDForum.Data;
using LDForum.Models.Forum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LDForum.Models.Post;
using LDForum.Data.Models;

namespace LDForum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumService;
        private readonly IPost _postService;

        public ForumController(IForum forumService )
        {
            _forumService = forumService;
            
        }
 
        public IActionResult Index()
        {
            var forums = _forumService.GetAll().Select(forum => new ForumListingModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description
            });
            var model = new ForumIndexModel
            {
                ForumList = forums
            };
            return View(model);
        }

        public IActionResult Topic(int id)
        {
            var forum = _forumService.GetById(id);
            var posts = forum.Posts;
            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                AuthorName= post.User.UserName,
                Title = post.Title,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = BuildForumListing(post)

            });
            var model = new ForumTopicModel
            {
                Posts = postListings,
                Forum = BuildForumListing(forum)
            };
            return View(model);
        }
        private ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;
            return BuildForumListing(forum);
        }
        private ForumListingModel BuildForumListing(Forum forum)
        {

            return new ForumListingModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImagesUrl
            };
        }



        // GET: Forum/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Forum/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Forum/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Forum/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Forum/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Forum/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Forum/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}