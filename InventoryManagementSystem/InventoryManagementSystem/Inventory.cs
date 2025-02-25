using System.ComponentModel;

namespace InventoryManagementSystem
{
    public static class Inventory
    {
        public static BindingList<Part> AllParts = new BindingList<Part>();
        public static BindingList<Product> Products = new BindingList<Product>();

        private static int nextPartID = 1;
        public static int GenerateNewPartID() => nextPartID++;

        private static int nextProductID = 1;
        public static int GenerateNewProductID() => nextProductID++;

        public static void AddPart(Part part) => AllParts.Add(part);

        public static bool DeletePart(int partID)
        {
            Part part = LookupPart(partID);
            if (part != null)
            {
                AllParts.Remove(part);
                return true;
            }
            return false;
        }

        public static Part LookupPart(int partID)
        {
            foreach (Part part in AllParts)
            {
                if (part.PartID == partID)
                {
                    return part;
                }
            }
            return null;
        }

        public static void UpdatePart(int partID, Part updatedPart)
        {
            for (int i = 0; i < AllParts.Count; i++)
            {
                if (AllParts[i].PartID == partID)
                {
                    AllParts[i] = updatedPart;
                    break;
                }
            }
        }

        public static void AddProduct(Product product) => Products.Add(product);

        public static bool DeleteProduct(int productID)
        {
            Product product = LookupProduct(productID);
            if (product != null)
            {
                Products.Remove(product);
                return true;
            }
            return false;
        }

        public static Product LookupProduct(int productID)
        {
            foreach (Product product in Products)
            {
                if (product.ProductID == productID)
                {
                    return product;
                }
            }
            return null;
        }

        public static void UpdateProduct(int productID, Product updatedProduct)
        {
            for (int i = 0; i < Products.Count; i++)
            {
                if (Products[i].ProductID == productID)
                {
                    Products[i] = updatedProduct;
                    break;
                }
            }
        }
    }
}
