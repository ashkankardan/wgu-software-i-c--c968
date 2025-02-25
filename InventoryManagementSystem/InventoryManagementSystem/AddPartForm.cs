using System.Windows.Forms;
using System;

namespace InventoryManagementSystem
{
    public partial class AddPartForm : Form
    {
        public AddPartForm()
        {
            InitializeComponent();
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
                Part newPart;

                int partID = Inventory.GenerateNewPartID();
                string name = txtName.Text;
                int inStock = int.Parse(txtInventory.Text);
                decimal price = decimal.Parse(txtPrice.Text);
                int max = int.Parse(txtMax.Text);
                int min = int.Parse(txtMin.Text);

                if (min > max)
                {
                    MessageBox.Show("Minimum value cannot be greater than maximum value.");
                    return;
                }

                if (radioInHouse.Checked)
                {
                    int machineID = int.Parse(txtMachineIDorCompanyName.Text);
                    newPart = new InHousePart(partID, name, inStock, price, max, min, machineID);
                }
                else
                {
                    string companyName = txtMachineIDorCompanyName.Text;
                    newPart = new OutsourcedPart(partID, name, inStock, price, max, min, companyName);
                }

                Inventory.AddPart(newPart);
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
