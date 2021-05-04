using System.Linq;
using GbWebApp.Domain.DTO;
using GbWebApp.Domain.Entities;
using System.Collections.Generic;

namespace GbWebApp.Services.Mappers
{
    public static class SectionMapper
    {
        public static SectionDTO ToDTO(this Section section) => section is null ? null : new SectionDTO
        {
            Id = section.Id,
            Name = section.Name,
            Order = section.Order,
            ParentId = section.ParentId,
            ProductCnt = section.Products.Count,
        };

        public static Section FromDTO(this SectionDTO section) => section is null ? null : new Section
        {
            Id = section.Id,
            Name = section.Name,
            Order = section.Order,
            ParentId = section.ParentId,
        };

        public static IEnumerable<SectionDTO> ToDTO(this IEnumerable<Section> sections) => sections.Select(ToDTO);

        public static IEnumerable<Section> FromDTO(this IEnumerable<SectionDTO> sections) => sections.Select(FromDTO);
    }
}
