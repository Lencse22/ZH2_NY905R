using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZH2_NY905R
{
    public partial class FormCustomer : Form
    {
        public FormCustomer()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBox1, "");
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (ValidateName(textBox1.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox1, "Not a name");
            }
        }

        bool ValidateName(string name)
        {
            Regex r = new Regex("^[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+");
            return !r.IsMatch(name);
        }
    }
}
