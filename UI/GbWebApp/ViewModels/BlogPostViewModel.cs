using System;

namespace GbWebApp.ViewModels
{
    public record BlogPostViewModel
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public string AuthorId { get; init; }

        public DateTime Created { get; init; }

        public double Rating { get; init; }

        public string Image { get; init; }

        public string Content { get; init; }
    }
}
