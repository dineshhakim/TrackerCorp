using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;

namespace Tracker.Blog.Models
{

    public class PostRepository : IDisposable
    {
        private BlogContext _blogContext;

        public PostRepository()
        {
            _blogContext = new BlogContext();
        }
        /// <summary>
        /// Gets the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Post Get(int id)
        {
            return _blogContext.Posts.Include(x => x.Comments).FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        public IQueryable<Post> All
        {
            get { return _blogContext.Posts; }
        }
        /// <summary>
        /// Create Or Update 
        /// </summary>
        /// <param name="post"></param>
        public void CreateOrUpdate(Post post)
        {
            if (post.Id == default(int))
            {
                // New entity
                _blogContext.Posts.Add(post);
            }
            else
            {
                // Existing entity
                _blogContext.Entry(post).State = EntityState.Modified;
            }
        }
        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var postToDelete = Get(id);
            _blogContext.Posts.Remove(postToDelete);
        }
        /// <summary>
        /// Save changes 
        /// </summary>
        public void Save()
        {
            _blogContext.SaveChanges();
        }





        public void Dispose()
        {
            _blogContext.Dispose();
        }
    }
}