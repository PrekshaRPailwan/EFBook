using System;
using System.Collections.Generic;

namespace EFBook.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int Isbn { get; set; }

    public DateTime? PublicationDate { get; set; }

    public decimal Price { get; set; }

    public string? Language { get; set; }

    public string? Publisher { get; set; }

    public int? PageCount { get; set; }

    public double? AverageRating { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? GenreId { get; set; }

    public bool? IsPresent { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
