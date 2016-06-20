using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Tracker.Blog.Models
{
    public class CommentRepository : IDisposable
    {
        private BlogContext _blogContext;

        public CommentRepository()
        {
            _blogContext = new BlogContext();
        }
        /// <summary>
        /// Gets the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Comment Get(int id)
        {
            return _blogContext.Comments.Include(x => x.Post).FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        public IQueryable<Comment> GetAllByPostId(int id)
        {
            return _blogContext.Comments.Where(x => x.Post.Id == id);
        }
        /// <summary>
        /// Create Or Update 
        /// </summary>
        /// <param name="comment"></param>
        public void CreateOrUpdate(Comment comment)
        {
            if (comment.Id == default(int))
            {
                // New entity
                _blogContext.Comments.Add(comment);
            }
            else
            {
                // Existing entity
                _blogContext.Entry(comment).State = EntityState.Modified;
            }
        }
        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var postToDelete = Get(id);
            _blogContext.Comments.Remove(postToDelete);
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