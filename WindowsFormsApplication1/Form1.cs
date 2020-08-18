using System;
using System.Windows.Forms;
using BL;


namespace WindowsFormsApplication1
{
    public partial class FrmMainWindow : Form
    {
        public FrmMainWindow()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();

            Tabela TB = new Tabela();
            try
            {
                TB.FillGrid(dataGridView1);
                if (dataGridView1.RowCount == 1)
                {
                    MessageBox.Show("Baza de date este goala!", "Atentie", MessageBoxButtons.OK);
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Doriti sa inchideti Agenda Telefonica?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
                Application.Exit();
        }

        private void FrmMainWindow_Load(object sender, EventArgs e)
        {
            Tabela TB = new Tabela();
            try
            {
                TB.FillGrid(dataGridView1);
                if (dataGridView1.RowCount == 1)
                {
                    MessageBox.Show("Baza de date este goala!", "Atentie", MessageBoxButtons.OK);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            Tabela TB = new Tabela();

            try
            {
                TB.UpdateSQLBase(dataGridView1);
                MessageBox.Show("Modificarile au fost salvate in baza de date", "Update Reusit", MessageBoxButtons.OK);

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

        private void serializareInformatiiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Tabela TB = new Tabela();
                TB.FillListaPersoana(dataGridView1);
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Title = "Salvare fisier serializare";
                saveFile.Filter = "XML files|*.xml";
                saveFile.InitialDirectory = Application.StartupPath;
                saveFile.DefaultExt = "xml";
                saveFile.FileName = "Abonati_" + DateTime.Now.ToString("yyyyMMddHHmmss");

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    TB.write(saveFile);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void cautaPersoanaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSearchWindow frmSearch = new frmSearchWindow();
            frmSearch.Show();


        }


        private void fontOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();

            if (fd.ShowDialog() != DialogResult.Cancel)
            {
                dataGridView1.Font = fd.Font;
                btnLoad.Font = fd.Font;
                btnSave.Font = fd.Font;
            }

        }

        private void colorOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            
            if(cd.ShowDialog() != DialogResult.Cancel)
            {
                this.BackColor = cd.Color;
            }
        }
    }
}
