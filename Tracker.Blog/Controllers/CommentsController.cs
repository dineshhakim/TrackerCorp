using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Tracker.Blog.Models;

namespace Tracker.Blog.Controllers
{
    public class CommentsController : Controller
    {
        private readonly CommentRepository _repo = new CommentRepository();
        private readonly PostRepository _repoPost = new PostRepository();


        // GET: Comments/Create
        public ActionResult Create(int id)
        {

            var post = _repoPost.Get(id);
            var comment = new Comment { Post = post };
            return View(comment);
        }

        // POST: Comments/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,PostId,UserName,Message")] Comment comment)
        {
            comment.CreatedDate = DateTime.Now;
            comment.Id = 0;
            if (ModelState.IsValid)
            {
                _repo.CreateOrUpdate(comment);
                _repo.Save();
                return RedirectToAction("Details",  "Post", new { Id = comment.PostId });
            }
            return View(comment);
        }

    }
}
