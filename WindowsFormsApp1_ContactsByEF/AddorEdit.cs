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
    public partial class AddorEdit : Form
    {
        public int IDAddorEdit = 0;
        public AddorEdit()
        {
            InitializeComponent();
        }

        private void AddorEdit_Load(object sender, EventArgs e)
        {
            if (IDAddorEdit==0)
            {
                this.Text = "افزودن مخاطب جدید";
            }
            else
            {
                this.Text = "ویرایش";
                using (ContactsByEFEntities EF = new ContactsByEFEntities())
                {
                    var EditInfo = EF.ContactsByEFTable.FirstOrDefault(m => m.ID == IDAddorEdit);
                    txtName.Text = EditInfo.Name;
                    txtFamily.Text = EditInfo.Family;
                    txtNumber.Text = EditInfo.Number;
                }
            }
        }

        public bool isTextBoxFilled()
        {
            bool Validation = true;
            if (txtName.Text == "" || txtFamily.Text == "" || txtNumber.Text == "")
            {
                Validation = false;
                MessageBox.Show("جاهای خالی که با ستاره مشخص شده است را پر کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return Validation;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (isTextBoxFilled() == true)
            {
                if (IDAddorEdit == 0)
                {
                    ContactsByEFTable P1 = new ContactsByEFTable()
                    {
                        Name = txtName.Text,
                        Family = txtFamily.Text,
                        Number = txtNumber.Text
                    };
                    using (ContactsByEFEntities EF = new ContactsByEFEntities())
                    {
                        EF.ContactsByEFTable.Add(P1);
                        EF.SaveChanges();
                    }
                    MessageBox.Show("باموفقیت ثبت شد", "ثبت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    using (ContactsByEFEntities EF = new ContactsByEFEntities())
                    {
                        var EditInfo = EF.ContactsByEFTable.FirstOrDefault(m => m.ID == IDAddorEdit);
                        if (EditInfo!=null)
                        {
                            EditInfo.Name = txtName.Text;
                            EditInfo.Family = txtFamily.Text;
                            EditInfo.Number = txtNumber.Text;
                            EF.SaveChanges();
                            MessageBox.Show("باموفقیت ثبت شد", "ثبت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult = DialogResult.OK;
                        }
                    }
                }
            }
        }
    }
}
