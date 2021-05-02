using Microsoft.AspNetCore.Mvc;
using GbWebApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using GbWebApp.Interfaces.Services;

namespace GbWebApp.Controllers
{
    public class BlogController : Controller
    {
        //readonly IBlogData __blogData;
        readonly IAnyEntityCRUD<BlogPost> __blogData;

        public BlogController(IAnyEntityCRUD<BlogPost> blogData) { __blogData = blogData; }

        public IActionResult Index() => View(__blogData.Get().AsQueryable().Include(a => a.Author));

        public IActionResult BlogSingle(int id) => View(__blogData.Get().AsQueryable().Include(a => a.Author).FirstOrDefault(p => p.Id == id));
    }
}
