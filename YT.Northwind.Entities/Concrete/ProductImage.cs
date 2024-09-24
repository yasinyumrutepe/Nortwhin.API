

namespace Northwind.Entities.Concrete
{
    public class ProductImage
    {
        public int ProductImageID { get; set; }
        public int ProductID { get; set; }
        public string ImagePublicID { get; set; }
        public string ImagePath { get; set; }

    }
}
