using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tracker.Blog.Models;

namespace Tracker.Blog.Controllers
{
    public class PostController : Controller
    {
        private PostRepository _repo = new PostRepository();
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            return View(_repo.All.Include(x => x.Comments));
        }
        /// <summary>
        /// Detailses the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ViewResult Details(int id)
        {
            Post post = _repo.Get(id);
            return View(post);
        }
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Creates the specified post.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Post post)
        {
            post.CreatedOn = DateTime.Now;
            if (ModelState.IsValid)
            {
                _repo.CreateOrUpdate(post);
                _repo.Save();
                return RedirectToAction("Index");
            }
            return View(post);
        }
        /// <summary>
        /// Edits the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            Post post = _repo.Get(id);
            return View(post);
        }
        /// <summary>
        /// Edits the specified post.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                var existingPost = _repo.Get(post.Id);
                existingPost.Title = post.Title;
                existingPost.Body = post.Body;
                _repo.CreateOrUpdate(existingPost);
                _repo.Save();
                return RedirectToAction("Index");
            }
            return View(post);
        }
        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            Post post = _repo.Get(id);
            return View(post);
        }
        /// <summary>
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            _repo.Save();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Releases unmanaged resources and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _repo.Dispose();
            base.Dispose(disposing);
        }
    }
}