using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class frmSearchWindow : Form
    {
        public frmSearchWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tabela Tab = new Tabela();
            try
            {
            Tab.Search(dataGridView1, textBox1.Text);
                if (dataGridView1.RowCount == 1)
                {
                    MessageBox.Show("Nici un rezultat gasit", "Atentie", MessageBoxButtons.OK);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show("Eroare de conexiune", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
