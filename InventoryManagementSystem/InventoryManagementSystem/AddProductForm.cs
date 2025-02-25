using System.Windows.Forms;
using System;
using System.Linq;

namespace InventoryManagementSystem
{
    public partial class AddProductForm : Form
    {
        private Product currentProduct;

        public AddProductForm()
        {
            InitializeComponent();
            currentProduct = new Product();
            dgvAssociatedParts.DataSource = currentProduct.AssociatedParts;
        }

        private void btnAddPart_Click(object sender, EventArgs e)
        {
            if (dgvAllParts.SelectedRows.Count > 0)
            {
                Part selectedPart = (Part)dgvAllParts.SelectedRows[0].DataBoundItem;
                currentProduct.AddAssociatedPart(selectedPart);
            }
        }

        private void btnDeletePart_Click(object sender, EventArgs e)
        {
            if (dgvAssociatedParts.SelectedRows.Count > 0)
            {
                Part selectedPart = (Part)dgvAssociatedParts.SelectedRows[0].DataBoundItem;
                currentProduct.RemoveAssociatedPart(selectedPart.PartID);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                currentProduct.Name = txtName.Text;
                currentProduct.InStock = int.Parse(txtInventory.Text);
                currentProduct.Price = decimal.Parse(txtPrice.Text);
                currentProduct.Max = int.Parse(txtMax.Text);
                currentProduct.Min = int.Parse(txtMin.Text);

                Inventory.AddProduct(currentProduct);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please ensure all fields are filled correctly: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.ToLower();
            var filteredParts = Inventory.AllParts.Where(p => p.Name.ToLower().Contains(searchQuery)).ToList();
            dgvAllParts.DataSource = filteredParts;
        }
    }
}
