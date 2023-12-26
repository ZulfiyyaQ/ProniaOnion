namespace ProniaOnion.Domain.Entities
{
    public class Color:BaseNameableEntity
    {
        //Relational props

        public ICollection<ProductColor>? ProductColors { get; set; }
    }
}
