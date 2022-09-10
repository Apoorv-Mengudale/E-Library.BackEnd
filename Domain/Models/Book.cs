namespace Domain.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string IntrestedArea { get; set; }
        public int YearOfIssue { get; set; }
        public int NoOfCopies { get; set; }

    }
}
