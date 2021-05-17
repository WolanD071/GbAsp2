using GbWebApp.Domain.Entities;

namespace GbWebApp.ViewModels
{
    public class BreadCrumbsViewModel
    {
        public string Controller { get; set; }

        public string ActionName { get; set; }

        public string ActionDisp { get; set; }

        public Section Section { get; set; }

        public Brand Brand { get; set; }

        public Product Product { get; set; }
    }
}
