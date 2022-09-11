namespace Domain.Models.Books;

public class UpdateBookRequest
{
    public string BookName { get; set; }
    public string AuthorName { get; set; }
    public string IntrestedArea { get; set; }
    public string NoOfCopies { get; set; }
    public string YearOfIssue { get; set; }
}