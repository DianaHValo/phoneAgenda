using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.IO;

namespace BL
{
    [Serializable]
    public class Tabela
    {
        public List<Persoana> ListaPersoana = new List<Persoana>();

        DataTable dt;
        SqlDataAdapter sqlAd;
        SqlCommandBuilder commBuild;
        string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\andre\\Desktop\\Bit Project\\Agenda.mdf\";Integrated Security=True;Connect Timeout=30";

        public void FillListaPersoana(DataGridView DataGrid1)
        {
            for (int rows = 0; rows < DataGrid1.Rows.Count; rows++)
            {
                Persoana pers = new Persoana();

                string convert = Convert.ToString(DataGrid1.Rows[rows].Cells["Nume"].Value);
                pers.nume = convert;
                string conversie = Convert.ToString(DataGrid1.Rows[rows].Cells["Numar"].Value);
                pers.numarTel = conversie;
                ListaPersoana.Add(pers);
            }
        }

        public void FillGrid(DataGridView DataView)
        {
            dt = new DataTable("tabel");
            sqlAd = new SqlDataAdapter("SELECT * FROM T1", connString);

            commBuild = new SqlCommandBuilder(sqlAd);

            sqlAd.Fill(dt);
            DataView.DataSource = dt;
        }

        public void UpdateSQLBase(DataGridView DataView)
        {
            sqlAd = new SqlDataAdapter("SELECT * FROM T1", connString);

            commBuild = new SqlCommandBuilder(sqlAd);
            commBuild.GetUpdateCommand();
            sqlAd.Update((DataTable)DataView.DataSource);
        }

        public void write(SaveFileDialog SVD)
        {
            Stream myStream = null;

            if(ListaPersoana.Count == 0)
            {
                throw new Exception("Nimic de serializat!");
            }

            if ((myStream = SVD.OpenFile()) != null)
            {
                var xmlFormat = new XmlSerializer(typeof(List<Persoana>));
                xmlFormat.Serialize(myStream, ListaPersoana);
                myStream.Close();
            }
        }

        public void Search(DataGridView DtView, string searchquery)
        {
            dt = new DataTable("tabel");

            sqlAd = new SqlDataAdapter($"SELECT * From T1 WHERE Nume Like '%{searchquery}%'", connString);

            commBuild = new SqlCommandBuilder(sqlAd);

            sqlAd.Fill(dt);
            DtView.DataSource = dt;
        }
    }
}
