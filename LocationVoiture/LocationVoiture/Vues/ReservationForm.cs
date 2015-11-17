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
    public partial class ReservationForm : Form
    {

        // Attributs

        private static string OPERATION_CLIENT_CREATION = "Création";
        private static string OPERATION_CLIENT_UPDATE = "Updater";
        private static string FIND_BY_CLIENT_ID = "client id";

        // Propriétés

        private ClientsServices clientServices { get; set; }
        private VehiculeServices vehiculeServices { get; set; }
        private SuccursalesServices succursalesService { get; set; }

        private List<vehicule> listeVehicule { get; set; }

        // Constructeur

        public ReservationForm(String operation)
        {
            InitializeComponent();
            
            clientServices = new ClientsServices();
            vehiculeServices = new VehiculeServices();
            succursalesService = new SuccursalesServices();

            fillTheComboBox();
            lblClientCreate_operation.Text = operation;

            // Selection de l'affichage
            if (operation.Equals(OPERATION_CLIENT_CREATION))
            {
                setFieldsVisibility(true);
                btnClientCreate_add.Text = ReservationForm.OPERATION_CLIENT_UPDATE;
            }            

        }

        // Méthodes

        /// <summary>
        /// Création d'un nouveau client à l'aide des informations inscrites dans les champs
        /// </summary>        
        private void btnClientCreate_add_Click(object sender, EventArgs e)
        {

            if (!btnClientCreate_add.Text.Equals(ReservationForm.OPERATION_CLIENT_UPDATE))
            {
                client addClient = new client();

                addClient.nom = txtClientCreate_nom.Text;
                addClient.prenom = txtClientCreate_prenom.Text;
                addClient.telephone = txtClientCreate_phone.Text;
                addClient.adresse_client = txtClientCreate_adresse.Text;
                addClient.courriel = txtClientCreate_email.Text;

                if (clientServices.addClient(addClient))
                {
                    emptyClientFormFields();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Une erreur est survenue lors de la creation du client");
                }
            }
            else
            {
                string idToUpdate = txtClientCreate_idSearch.Text;

                List<client> listeClients = clientServices.find(idToUpdate, ReservationForm.FIND_BY_CLIENT_ID);
                if(listeClients.Count > 0)
                {
                    client clientToUpdate = listeClients[0];

                    clientToUpdate.nom = txtClientCreate_nom.Text;
                    clientToUpdate.prenom = txtClientCreate_prenom.Text;
                    clientToUpdate.telephone = txtClientCreate_phone.Text;
                    clientToUpdate.adresse_client = txtClientCreate_adresse.Text;
                    clientToUpdate.courriel = txtClientCreate_email.Text;
                }


                clientServices.Save();
                MessageBox.Show("Le client a été modifé");
            }
        }

        /// <summary>
        /// Recherche d'un client afin de modifier ses informations.
        /// Si le champ est vide, un nouveau formulaire de recherche est lancé afin de permettre la recherche à l'aide de plus de champs
        /// Sinon, la recherche est effectué à l'aide de l'id du client.
        /// </summary>        
        private void btnClientForm_Find_Click(object sender, EventArgs e)
        {

            List<client> listeClients = new List<client>();
            String searchValue = "";

            if (txtClientCreate_idSearch.Text == "")
            {
                ClientSearch searchForm = new ClientSearch();
                searchForm.Owner = this;
                searchForm.ShowDialog();

                // récupération du ID sélectionner dans le searchForm
                searchValue = searchForm.clientSearchID;
            }
            else
            {                
                // le id est lu directement dans le champ
                searchValue = txtClientCreate_idSearch.Text;
            }

            listeClients = clientServices.find(searchValue, ReservationForm.FIND_BY_CLIENT_ID);

            // Update du client
            if (listeClients.Count > 0)
            {
                client clientToUpdate = listeClients[0];

                txtClientCreate_clientId.Enabled = false;

                txtClientCreate_clientId.Text = clientToUpdate.clientID.ToString();
                txtClientCreate_nom.Text = clientToUpdate.nom;
                txtClientCreate_prenom.Text = clientToUpdate.prenom;
                txtClientCreate_phone.Text = clientToUpdate.telephone;
                txtClientCreate_adresse.Text = clientToUpdate.adresse_client;
                txtClientCreate_email.Text = clientToUpdate.courriel;
            }
            else
            {
                MessageBox.Show("Aucun client n'a pu etre trouvé");
            }
        }

        /// <summary>
        /// Cancel la création d'un client (fermeture du formulaire)
        /// </summary>       
        private void txtClientCreate_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region UTILITAIRES

        private void fillTheComboBox()
        {

            listeVehicule = new List<vehicule>();

            List<succursale> listeSuccursales = new List<succursale>();           
            listeSuccursales = succursalesService.getAllSuccursale();

            foreach (succursale succ in listeSuccursales)
            {
                cbReservationCreate_Succursale.Items.Add(succ.succursaleID + " - " + succ.nom);
            }

            cbReservationCreate_Succursale.SelectedIndex = 0;

            int succID = int.Parse(cbReservationCreate_Succursale.SelectedItem.ToString().Substring(0, 1));            
            listeVehicule = vehiculeServices.getVehiculeFromSuccursale(succID);

        }

        private void emptyClientFormFields()
        {
            txtClientCreate_idSearch.Text = "";
            txtClientCreate_nom.Text = "";
            txtClientCreate_prenom.Text = "";
            txtClientCreate_phone.Text = "";
            txtClientCreate_adresse.Text = "";
            txtClientCreate_email.Text = "";
        }

        private void setFieldsVisibility(bool visibilityChoice)
        {            
            btnClientForm_Find.Visible = visibilityChoice;
            txtClientCreate_idSearch.Visible = visibilityChoice;
            txtClientCreate_clientId.Enabled = visibilityChoice;
            lblClientCreate_id.Visible = visibilityChoice;
            panelClientForm_id.Visible = visibilityChoice;
        }

        #endregion UTILITAIRES

        private void cbReservation_marque_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(cbReservationCreate_marque.SelectedItem.ToString());
        }

        private void cbReservationCreate_Succursale_SelectedIndexChanged(object sender, EventArgs e)
        {

            cbReservationCreate_marque.Items.Clear();

            int succID = int.Parse(cbReservationCreate_Succursale.SelectedItem.ToString().Substring(0, 1));

            List<fabriquant> listeFabriquant = new List<fabriquant>();
            listeFabriquant = vehiculeServices.getDistinctFabriquant(succID);

            List<modele> listeModele = new List<modele>();
            listeModele = vehiculeServices.getDistinctModele(succID);

            if (cbReservationCreate_Succursale.SelectedItem.ToString() != null)
            {
                foreach (fabriquant fab in listeFabriquant)
                {
                     cbReservationCreate_marque.Items.Add(fab.nom_fabriquant);
                    
                }
            }

            try
            {
                cbReservationCreate_marque.SelectedIndex = 0;
            } catch
            {
                cbReservationCreate_marque.ResetText();
                cbReservationCreate_marque.SelectedIndex = -1;
                
            }

            if (cbReservationCreate_marque.SelectedItem.ToString() != null)
            {
                foreach (modele model in listeModele)
                {
                    cbReservationCreate_model.Items.Add(model.nom_modele);

                }
            }

            try
            {
                cbReservationCreate_model.SelectedIndex = 0;
            }
            catch
            {
                cbReservationCreate_model.ResetText();
                cbReservationCreate_model.SelectedIndex = -1;

            }


        }

        private void cbReservationCreate_nbPassager_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
