using LocationVoiture.Model;
using LocationVoiture.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocationVoiture.Vues
{
    public partial class ClientSearch : Form
    {

        private ClientsServices clientServices { get; set; }
        public String clientSearchID { get; private set; }

        public ClientSearch()
        {
            InitializeComponent();

            clientServices = new ClientsServices();

            btnClientSearch_select.Enabled = false;
            comboBox1.Items.Add("client id");
            comboBox1.Items.Add("nom");
            comboBox1.Items.Add("prenom");
            comboBox1.Items.Add("courriel");
            comboBox1.Items.Add("telephone");
            comboBox1.SelectedIndex = 0;

        }

        /// <summary>
        /// SEARCH
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            String searchValue = txtClientSearch_value.Text;
            String comboChoice = comboBox1.SelectedItem.ToString();
            List<client> clientFound = new List<client>();

            switch (comboChoice)
            {
                case "client id":
                    clientFound = clientServices.find(searchValue, "client id");
                    break;
                case "nom":
                    clientFound = clientServices.find(searchValue, "nom");
                    break;
                case "prenom":
                    clientFound = clientServices.find(searchValue, "prenom");
                    break;
                case "courriel":
                    clientFound = clientServices.find(searchValue, "courriel");
                    break;
                case "telephone":
                    clientFound = clientServices.find(searchValue, "telephone");
                    break;
            }


            if (clientFound != null)
            {
                dataGridView1.DataSource = clientFound;
                btnClientSearch_select.Enabled = true;

                // hiding columns (6:password | 7:assurance | 8:permis_conduire_num | 9:num_carte_credit)
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;

            }
            else
            {
                MessageBox.Show("No client with this name");
            }
            
        }

        /// <summary>
        /// CLOSE
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClientSearch_select_Click(object sender, EventArgs e)
        {
            int selectedRow = dataGridView1.CurrentRow.Index;
            object selectedRowID = dataGridView1[0, selectedRow].Value;
            String clientID = selectedRowID.ToString();

            clientSearchID = clientID;

            this.Close();
        }
    }
}
