using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZH2_NY905R
{
    public partial class FormProduct : Form
    {
        public int price = 0;
        public FormProduct()
        {
            InitializeComponent();
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox1, "Product name cannot be empty!");
            }
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBox1, "");
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {

            if(!(int.TryParse(textBox2.Text, out price)) || price <= 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox2, "Product price should be a number higher than 0!");
            }
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBox2, "");
        }

        private void FormProduct_Load(object sender, EventArgs e)
        {
            //Ez sokkal szebb lett volna egy UnitType táblával :(
            string[] UnitTypes = {"csomag","darab","kilogramm","liter"};
            listBox1.DataSource = UnitTypes.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
