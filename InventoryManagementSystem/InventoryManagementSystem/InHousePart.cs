namespace InventoryManagementSystem
{
    public class InHousePart : Part
    {
        public int MachineID { get; set; }

        public InHousePart(int partID, string name, int inStock, decimal price, int max, int min, int machineID)
            : base(partID, name, inStock, price, max, min)
        {
            MachineID = machineID;
        }

        public InHousePart() : base() { }
    }
}
