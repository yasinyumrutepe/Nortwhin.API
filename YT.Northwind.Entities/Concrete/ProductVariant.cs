
namespace Northwind.Entities.Concrete
{
    public class ProductVariant
    {
        public int ProductVariantID { get; set; }
        public int ProductID { get; set; }
        public int VariantID { get; set; }
        public virtual Variant Variant { get; set; }
        public virtual Product Product { get; set; }
    }
}
