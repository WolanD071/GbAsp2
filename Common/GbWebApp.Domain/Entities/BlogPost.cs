using System;
using GbWebApp.Domain.Entities.Base;
using GbWebApp.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GbWebApp.Domain.Entities
{
    [Table("Blogs")]
    public class BlogPost : Entity
    {
        public string Title { get; set; }

        public string AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public User Author { get; set; }

        public DateTime Created { get; set; }

        public double Rating { get; set; }

        public string Image { get; set; }

        public string Content { get; set; }

        //public BlogPost() { }
    }
}
