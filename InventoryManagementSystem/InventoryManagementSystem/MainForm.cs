using System;
using System.Linq;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            dgvParts.DataSource = Inventory.AllParts;
            dgvProducts.DataSource = Inventory.Products;
        }

        private void btnAddPart_Click(object sender, EventArgs e)
        {
            var addPartForm = new AddPartForm();
            addPartForm.ShowDialog();
            dgvParts.Refresh();
        }

        private void btnModifyPart_Click(object sender, EventArgs e)
        {
            if (dgvParts.CurrentRow != null)
            {
                Part selectedPart = (Part)dgvParts.CurrentRow.DataBoundItem;
                var modifyPartForm = new ModifyPartForm(selectedPart);
                modifyPartForm.ShowDialog();
                dgvParts.Refresh();
            }
        }

        private void btnDeletePart_Click(object sender, EventArgs e)
        {
            if (dgvParts.CurrentRow != null)
            {
                Part selectedPart = (Part)dgvParts.CurrentRow.DataBoundItem;
                Inventory.DeletePart(selectedPart.PartID);
                dgvParts.Refresh();
            }
        }

        private void btnSearchPart_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearchPart.Text.ToLower();
            var filteredParts = Inventory.AllParts.Where(p => p.Name.ToLower().Contains(searchQuery)).ToList();
            dgvParts.DataSource = filteredParts;
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            var addProductForm = new AddProductForm();
            addProductForm.ShowDialog();
            dgvProducts.Refresh();
        }

        private void btnModifyProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow != null)
            {
                Product selectedProduct = (Product)dgvProducts.CurrentRow.DataBoundItem;
                var modifyProductForm = new ModifyProductForm(selectedProduct);
                modifyProductForm.ShowDialog();
                dgvProducts.Refresh();
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow != null)
            {
                Product selectedProduct = (Product)dgvProducts.CurrentRow.DataBoundItem;
                Inventory.DeleteProduct(selectedProduct.ProductID);
                dgvProducts.Refresh();
            }
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearchProduct.Text.ToLower();
            var filteredProducts = Inventory.Products.Where(p => p.Name.ToLower().Contains(searchQuery)).ToList();
            dgvProducts.DataSource = filteredProducts;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
