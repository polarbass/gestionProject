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
    public partial class MainForm : Form
    {

        private VehiculeServices vehiculeServices { get; set; }
        private ClientsServices clientServices { get; set; }

        private static string OPERATION_CLIENT_CREATION = "Création";
        private static string OPERATION_CLIENT_MODIFICATION = "Modification";

        public MainForm()
        {
            InitializeComponent();

            vehiculeServices = new VehiculeServices();
            clientServices = new ClientsServices();

            LoginForm form1 = new LoginForm();
            form1.Owner = this;
            form1.ShowDialog();

            lblMainForm_activeUser.Text = form1.username;            

            carPanel.Hide();
            panelReservation.Hide();

        }


        /*******************************************************************
                                    RESERVATIONS
        *******************************************************************/

        #region RESERVATION

            private void btnReservationCreate_Click(object sender, EventArgs e)
            {
                ReservationForm reservationForm = new ReservationForm("Création");
                reservationForm.Owner = this;
                reservationForm.ShowDialog();
            }

        #endregion RESERVATION


        /*******************************************************************
                                    CLIENTS
        *******************************************************************/

        #region CLIENT

            private void btnClientCreate_Click(object sender, EventArgs e)
            {
                ClientForm clientForm = new ClientForm(MainForm.OPERATION_CLIENT_CREATION);
                clientForm.Owner = this;
                clientForm.ShowDialog();
            }

            private void btnClientCreate_add_Click(object sender, EventArgs e)
            {

            }

            private void btnClientModify_Click(object sender, EventArgs e)
            {
                ClientForm clientForm = new ClientForm(MainForm.OPERATION_CLIENT_MODIFICATION);
                clientForm.Owner = this;
                clientForm.ShowDialog();
            }

            private void btnClientModify_update_Click(object sender, EventArgs e)
            {


            }

            private void btnClientModify_cancel_Click(object sender, EventArgs e)
            {
            }

            private void btnClientModify_find_Click(object sender, EventArgs e)
            {

            }

        #endregion CLIENT

        /*******************************************************************
                                    VEHICULES
        *******************************************************************/

        #region VEHICULE

            private void btnCarCreate_Click(object sender, EventArgs e)
                {
                    carPanel.Show();
                    panelReservation.Hide();
                }

                private void btnCarModify_Click(object sender, EventArgs e)
                {
                    ClientSearch test = new ClientSearch();
                    test.Owner = this;
                    test.ShowDialog();
                }

                private void btnCarAdd_Click(object sender, EventArgs e)
                {
                    try
                    {
                        vehicule vehicule = new vehicule();
                        //vehiclesServices.addVehicle(vehicule);
                    }
                    catch
                    {
                        MessageBox.Show("erreur");
                    }

                }

                private void btnCarCancel_Click(object sender, EventArgs e)
                {
                    carPanel.Hide();
                }

        #endregion VEHICULE

        #region SUCCURSALE
        #endregion SUCCURSALE



    }
}
