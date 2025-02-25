using System.Windows.Forms;
using System;

namespace InventoryManagementSystem
{
    public partial class ModifyPartForm : Form
    {
        private Part selectedPart;

        public ModifyPartForm(Part part)
        {
            InitializeComponent();
            selectedPart = part;
            InitializeFields();
        }

        private void InitializeFields()
        {
            if (selectedPart is InHousePart inHousePart)
            {
                radioInHouse.Checked = true;
                txtMachineIDorCompanyName.Text = inHousePart.MachineID.ToString();
            }
            else if (selectedPart is OutsourcedPart outsourcedPart)
            {
                radioOutsourced.Checked = true;
                txtMachineIDorCompanyName.Text = outsourcedPart.CompanyName;
            }

            txtName.Text = selectedPart.Name;
            txtInventory.Text = selectedPart.InStock.ToString();
            txtPrice.Text = selectedPart.Price.ToString();
            txtMax.Text = selectedPart.Max.ToString();
            txtMin.Text = selectedPart.Min.ToString();
        }

        private void radioInHouse_CheckedChanged(object sender, EventArgs e)
        {
            lblMachineIDorCompanyName.Text = "Machine ID";
        }

        private void radioOutsourced_CheckedChanged(object sender, EventArgs e)
        {
            lblMachineIDorCompanyName.Text = "Company Name";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                selectedPart.Name = txtName.Text;
                selectedPart.InStock = int.Parse(txtInventory.Text);
                selectedPart.Price = decimal.Parse(txtPrice.Text);
                selectedPart.Max = int.Parse(txtMax.Text);
                selectedPart.Min = int.Parse(txtMin.Text);

                if (selectedPart is InHousePart inHousePart)
                {
                    inHousePart.MachineID = int.Parse(txtMachineIDorCompanyName.Text);
                }
                else if (selectedPart is OutsourcedPart outsourcedPart)
                {
                    outsourcedPart.CompanyName = txtMachineIDorCompanyName.Text;
                }

                Inventory.UpdatePart(selectedPart.PartID, selectedPart);
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
    }
}
