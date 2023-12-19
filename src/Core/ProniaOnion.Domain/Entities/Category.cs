namespace ProniaOnion.Domain.Entities
{
    public class Category:BaseNameableEntity
    {   //Relational Props
        public ICollection<Product> Products { get; set; }
    }
}
