using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1_ContactsByEF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void dgvBulder()
        {
            using (ContactsByEFEntities EFConection = new ContactsByEFEntities())
            {
                dgvContactsList.AutoGenerateColumns = false;
                dgvContactsList.Columns[0].Visible = false;
                dgvContactsList.DataSource = EFConection.ContactsByEFTable.ToList();
            }

        }

        public bool isRowSelected()
        {
            if (dgvContactsList.CurrentRow!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvBulder();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            using (ContactsByEFEntities EFConection = new ContactsByEFEntities())
            {
                dgvContactsList.AutoGenerateColumns = false;
                dgvContactsList.Columns[0].Visible = false;
                dgvContactsList.DataSource = EFConection.ContactsByEFTable.Where(m=>m.Name.ToLower().Contains(txtSearch.Text.ToLower())
                     || m.Family.ToLower().Contains(txtSearch.Text.ToLower())
                     || m.Number.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dgvBulder();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (isRowSelected())
            {
                string FullName = dgvContactsList.CurrentRow.Cells[1].Value.ToString() + dgvContactsList.CurrentRow.Cells[2].Value.ToString();
                int ID = (int)dgvContactsList.CurrentRow.Cells[0].Value;
                if (MessageBox.Show($"حذف شود؟{FullName}","توجه",MessageBoxButtons.YesNo)==DialogResult.Yes)
                {                   
                    using (ContactsByEFEntities EFConection=new ContactsByEFEntities())
                    {
                        var DeleteInfo = EFConection.ContactsByEFTable.FirstOrDefault(m => m.ID == ID);
                        if (DeleteInfo!=null)
                        {
                            EFConection.ContactsByEFTable.Remove(DeleteInfo);
                            EFConection.SaveChanges();
                            dgvBulder();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("یک ردیف انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddorEdit frmAddorEdit = new AddorEdit();
            frmAddorEdit.ShowDialog();
            if (frmAddorEdit.DialogResult == DialogResult.OK) 
            {
                dgvBulder();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (isRowSelected())
            {
                string FullName = dgvContactsList.CurrentRow.Cells[1].Value.ToString() + dgvContactsList.CurrentRow.Cells[2].Value.ToString();
                int ID = (int)dgvContactsList.CurrentRow.Cells[0].Value;
                if (MessageBox.Show($"ویرایش شود؟{FullName}", "توجه", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    AddorEdit frmAddOrEdit = new AddorEdit();
                    frmAddOrEdit.IDAddorEdit = ID;
                    frmAddOrEdit.ShowDialog();
                    if (frmAddOrEdit.DialogResult == DialogResult.OK)
                    {
                        dgvBulder();
                    }
                }
            }
            else
            {
                MessageBox.Show("یک ردیف انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
