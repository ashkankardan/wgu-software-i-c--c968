using System;
using System.Linq;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class ModifyProductForm : Form
    {
        private Product currentProduct;

        public ModifyProductForm(Product product)
        {
            InitializeComponent();
            currentProduct = product;
            PopulateProductFields();
            RefreshPartGrids();
        }

        private void PopulateProductFields()
        {
            txtProductID.Text = currentProduct.ProductID.ToString();
            txtName.Text = currentProduct.Name;
            txtInventory.Text = currentProduct.InStock.ToString();
            txtPrice.Text = currentProduct.Price.ToString();
            txtMax.Text = currentProduct.Max.ToString();
            txtMin.Text = currentProduct.Min.ToString();
        }

        private void RefreshPartGrids()
        {
            dgvAllParts.DataSource = Inventory.AllParts;
            dgvAssociatedParts.DataSource = currentProduct.AssociatedParts;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.ToLower();
            var filteredParts = Inventory.AllParts
                .Where(p => p.Name.ToLower().Contains(searchQuery))
                .ToList();
            dgvAllParts.DataSource = filteredParts;
        }

        private void btnAddPart_Click(object sender, EventArgs e)
        {
            if (dgvAllParts.SelectedRows.Count > 0)
            {
                Part selectedPart = (Part)dgvAllParts.SelectedRows[0].DataBoundItem;
                currentProduct.AddAssociatedPart(selectedPart);
                RefreshPartGrids();
            }
            else
            {
                MessageBox.Show("Please select a part to add.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeletePart_Click(object sender, EventArgs e)
        {
            if (dgvAssociatedParts.SelectedRows.Count > 0)
            {
                Part selectedPart = (Part)dgvAssociatedParts.SelectedRows[0].DataBoundItem;
                if (currentProduct.RemoveAssociatedPart(selectedPart.PartID))
                {
                    RefreshPartGrids();
                }
                else
                {
                    MessageBox.Show("Failed to remove part. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a part to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int productID = int.Parse(txtProductID.Text);
                string name = txtName.Text;
                int inStock = int.Parse(txtInventory.Text);
                decimal price = decimal.Parse(txtPrice.Text);
                int max = int.Parse(txtMax.Text);
                int min = int.Parse(txtMin.Text);

                if (min > max)
                {
                    MessageBox.Show("Minimum value cannot be greater than the maximum value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (inStock < min || inStock > max)
                {
                    MessageBox.Show("Inventory must be between the minimum and maximum values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                currentProduct.ProductID = productID;
                currentProduct.Name = name;
                currentProduct.InStock = inStock;
                currentProduct.Price = price;
                currentProduct.Max = max;
                currentProduct.Min = min;

                Inventory.UpdateProduct(productID, currentProduct);
                MessageBox.Show("Product updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
