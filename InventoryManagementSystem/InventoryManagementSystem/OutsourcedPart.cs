namespace InventoryManagementSystem
{
    public class OutsourcedPart : Part
    {
        public string CompanyName { get; set; }

        public OutsourcedPart(int partID, string name, int inStock, decimal price, int max, int min, string companyName)
            : base(partID, name, inStock, price, max, min)
        {
            CompanyName = companyName;
        }

        public OutsourcedPart() : base() { }
    }
}
