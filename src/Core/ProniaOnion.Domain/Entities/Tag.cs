namespace ProniaOnion.Domain.Entities
{
    public class Tag : BaseNameableEntity
    {//Relational props
        public ICollection<ProductTag>? ProductTags { get; set; }
    }
}
