using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MarkdownSharp;
namespace Tracker.Blog.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(4000)]
        public string Body { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }

        public string BodyHtml(){
            var markdown = new Markdown();
            return markdown.Transform(Body);
        }

        public Post()
        {
            Comments= new List<Comment>();
        }
        public List<Comment> Comments { get; set; }
    }

}
