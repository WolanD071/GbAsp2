using GbWebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GbWebApp.Domain.Entities;
using System.Collections.Generic;
using GbWebApp.Interfaces.Services;

namespace GbWebApp.ServiceHosting.Controllers
{
    /// <summary> blog management </summary>
    [Route(WebApiRoutes.BlogPostsAPI)]
    [ApiController]
    public class BlogPostsApiController : ControllerBase, IAnyEntityCRUD<BlogPost>
    {
        private readonly IAnyEntityCRUD<BlogPost> _blogPostsData;

        public BlogPostsApiController(IAnyEntityCRUD<BlogPost> blogPostsData) => _blogPostsData = blogPostsData;

        /// <summary> adding an blogpost </summary>
        /// <param name="blogPost"> blogpost itself </param>
        /// <returns>id of the blogpost have been added </returns>
        [HttpPost]
        public int Add(BlogPost blogPost) => _blogPostsData.Add(blogPost);

        /// <summary> deletion of the blogpost with given id </summary>
        /// <param name="id"> id of the blogpost </param>
        /// <returns> bool - success or fail </returns>
        [HttpDelete("{id}")]
        public bool Delete(int id) => _blogPostsData.Delete(id);

        /// <summary> getting the all blogpost's list from database </summary>
        /// <returns> list of blogposts </returns>
        [HttpGet]
        public IEnumerable<BlogPost> Get() => _blogPostsData.Get();

        /// <summary> getting the blogpost by its id </summary>
        /// <param name="id"> given id </param>
        /// <returns> blogpost found </returns>
        [HttpGet("{id}")]
        public BlogPost Get(int id) => _blogPostsData.Get(id);

        /// <summary> updating info about the blogpost with given id </summary>
        /// <param name="blogPost">blogpost</param>
        [HttpPut]
        public void Update(BlogPost blogPost) => _blogPostsData.Update(blogPost);
    }
}
