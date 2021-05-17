using System.Net.Http;
using GbWebApp.Interfaces;
using GbWebApp.Clients.Base;
using GbWebApp.Domain.Entities;
using System.Collections.Generic;
using GbWebApp.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace GbWebApp.Clients.BlogPosts
{
    public class BlogPostsClient : BaseClient, IAnyEntityCRUD<BlogPost>
    {
        private readonly ILogger<BlogPostsClient> _logger;

        public BlogPostsClient(IConfiguration configuration, ILogger<BlogPostsClient> logger)
            : base(configuration, WebApiRoutes.BlogPostsAPI) => _logger = logger;

        public IEnumerable<BlogPost> Get() => Get<IEnumerable<BlogPost>>(Address);

        public BlogPost Get(int id) => Get<BlogPost>($"{Address}/{id}");

        public int Add(BlogPost blogPost)
        {
            _logger.LogInformation("Creating new post in blog...");
            using (_logger.BeginScope("*** CREATING BLOGPOST SCOPE ***"))
            {
                var result = Post(Address, blogPost).Content.ReadAsAsync<int>().Result;
                _logger.LogInformation($"...completed successfully! id={result}");
                return result;
            }
        }

        public void Update(BlogPost blogPost)
        {
            _logger.LogInformation($"Updating the blogpost with id={blogPost.Id}...");
            using (_logger.BeginScope("*** UPDATING BLOGPOST SCOPE ***"))
            {
                Put(Address, blogPost);
                _logger.LogInformation("...completed successfully!");
            }
        }

        public bool Delete(int id)
        {
            _logger.LogInformation($"Deleting the blogpost with id={id}...");
            using (_logger.BeginScope("*** DELETING BLOGPOST SCOPE ***"))
            {
                var result = Delete($"{Address}/{id}").IsSuccessStatusCode;
                _logger.LogInformation("{0}", result ? "...completed successfully!" : "such an blogpost not found!");
                return result;
            }
        }
    }
}
