using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GbWebApp.ViewModels
{
    public record SectionViewModel
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int Order { get; init; }
        public SectionViewModel Parent { get; init; }
        public List<SectionViewModel> ChildSections { get; } = new();
        public int ProductCount { get; init; }
        public int TotalProdCnt => ProductCount + ChildSections.Sum(s => s.ProductCount); // s => s.TotalProdCnt
    }
}
