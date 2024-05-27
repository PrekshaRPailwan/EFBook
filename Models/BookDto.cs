namespace EFBook.Models
{
    public class BookDto
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public int Isbn { get; set; }

        public DateTime? PublicationDate { get; set; }

        public decimal Price { get; set; }

        public string? Language { get; set; }

        public string? Publisher { get; set; }

        public int? PageCount { get; set; }

        public double? AverageRating { get; set; }

        public int? GenreId { get; set; }

        public List<int> AuthorIds { get; set; }
    }
}
