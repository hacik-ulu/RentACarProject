namespace RentACarProject.Domain.Entities;

// For Blogs.
public class Category
{
    public int CategoryID { get; set; }
    public string Name { get; set; }
    public List<Blog> Blogs { get; set; }
}