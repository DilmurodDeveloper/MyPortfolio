﻿namespace MyPortfolio.Shared.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public DateTimeOffset PublishedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
