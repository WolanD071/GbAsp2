using Microsoft.AspNetCore.Mvc;
using GbWebApp.Infrastructure.Interfaces;
using GbWebApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GbWebApp.Controllers
{
    public class BlogController : Controller
    {
        //readonly IBlogData __blogData;
        readonly IAnyEntityCRUD<BlogPost> __blogData;

        public BlogController(IAnyEntityCRUD<BlogPost> blogData) { __blogData = blogData; }

        public IActionResult Index() => View(__blogData.Get().Include(a => a.Author));

        public IActionResult BlogSingle(int id) => View(__blogData.Get().Include(a => a.Author).FirstOrDefault(p => p.Id == id));
    }
}
