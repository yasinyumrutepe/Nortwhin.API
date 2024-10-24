

namespace Northwind.Entities.Concrete
{
    public class Territory
    {
        public int TerritoryID { get; set; }
        public int RegionID { get; set; }
        public string TerritoryDescription { get; set; }
        public virtual Region Region { get; set; }

    }
}
