using LocationVoiture.Model;
using LocationVoiture.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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

        private static DateTime HEURE_OUVERTURE = DateTime.Parse("08:00:00");
        private static DateTime HEURE_FERMETURE = DateTime.Parse("21:00:00");
        private static double INTERVALLE_TEMPS = 60;

        // Propriétés

        private ClientsServices clientServices { get; set; }
        private VehiculeServices vehiculeServices { get; set; }
        private SuccursalesServices succursalesService { get; set; }
        private ModeleServices modelesServices { get; set; }
        private ReservationsServices reservationServices { get; set; }

        private List<vehicule> listeVehicule { get; set; }
        private List<fabriquant> listeFabriquant { get; set; }
        private List<modele> listeDesModeles { get; set; }

        // Constructeur

        public ReservationForm(String operation)
        {
            InitializeComponent();
            
            clientServices = new ClientsServices();
            vehiculeServices = new VehiculeServices();
            succursalesService = new SuccursalesServices();
            modelesServices = new ModeleServices();
            reservationServices = new ReservationsServices();

            fillTheComboBoxSuccursale();
            fillTheComboBoxOpenHours();
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

            reservation reservationToCreate = new reservation();

            reservationToCreate.clientID = int.Parse(txtClientCreate_clientId.Text);
            reservationToCreate.employeID = 2;
            reservationToCreate.succursaleID = ((KeyValuePair<int, string>) cbReservationCreate_Succursale.SelectedItem).Key;
            reservationToCreate.vehiculeID = ((KeyValuePair<int, string>)cbReservationCreate_noPlaque.SelectedItem).Key;

            DateTime reservationOUT = dateTimePicker_ReservationCreate_DateOUT.Value.Date.Add(TimeSpan.Parse("08:00:00"));
            DateTime reservationIN = dateTimePicker_ReservationCreate_DateIN.Value.Date.Add(TimeSpan.Parse("10:00:00"));

            reservationToCreate.date_debut_reservation = reservationOUT;
            reservationToCreate.date_fin_reservation = reservationIN;

            reservationToCreate.date_appel_reservation = DateTime.Now;

            reservationServices.AddReservation(reservationToCreate);

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

        private void fillTheComboBoxSuccursale()
        {

            listeVehicule = new List<vehicule>();

            List<succursale> listeSuccursales = new List<succursale>();           
            listeSuccursales = succursalesService.getAllSuccursale();

            Dictionary<int, string> dictionarySuccursales = new Dictionary<int, string>();

            foreach (succursale succ in listeSuccursales)
            {
                dictionarySuccursales.Add(succ.succursaleID, succ.nom);
            }

            cbReservationCreate_Succursale.DataSource = new BindingSource(dictionarySuccursales, null);
            cbReservationCreate_Succursale.DisplayMember = "Value";
            cbReservationCreate_Succursale.ValueMember = "Key";


            cbReservationCreate_Succursale.SelectedIndex = 0;

            int succID = (((KeyValuePair<int, string>)cbReservationCreate_Succursale.SelectedItem).Key);
            
            listeVehicule = vehiculeServices.getVehiculeFromSuccursale(succID);

        }

        /// <summary>
        /// Rempli les combobox HeureIN et HeureOUT de la réservation
        /// </summary>
        private void fillTheComboBoxOpenHours()
        {

            TimeSpan interval = TimeSpan.FromMinutes(ReservationForm.INTERVALLE_TEMPS);

            for (DateTime current = ReservationForm.HEURE_OUVERTURE; current <= ReservationForm.HEURE_FERMETURE; current += interval)
            {
                String stringTime = current.TimeOfDay.ToString().Substring(0,5);
                cbReservationCreate_HeureIN.Items.Add(stringTime);
                cbReservationCreate_HeureOUT.Items.Add(stringTime);                
            }

            cbReservationCreate_HeureIN.SelectedIndex = 0;
            cbReservationCreate_HeureOUT.SelectedIndex = 0;
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

        /// <summary>
        /// COMBOX choix de la succursale
        /// </summary>
        private void cbReservationCreate_Succursale_SelectedIndexChanged(object sender, EventArgs e)
        {

           int succID = (((KeyValuePair<int, string>)cbReservationCreate_Succursale.SelectedItem).Key);

            listeFabriquant = new List<fabriquant>();
            listeFabriquant = vehiculeServices.getDistinctFabriquant(succID);

            listeVehicule = new List<vehicule>();
            listeVehicule = vehiculeServices.getVehiculeFromSuccursale(succID);

            listeDesModeles = new List<modele>();

            // LISTE DES FABRICANTS

            Dictionary<int, string> dictionaryFabricants = new Dictionary<int, string>();

            if (cbReservationCreate_Succursale.SelectedItem.ToString() != null)
            {
                foreach (fabriquant fab in listeFabriquant)
                {
                    dictionaryFabricants.Add(fab.fabriquantID, fab.nom_fabriquant);
                }
            }

            cbReservationCreate_marque.DataSource = new BindingSource(dictionaryFabricants, null);
            cbReservationCreate_marque.DisplayMember = "Value";
            cbReservationCreate_marque.ValueMember = "Key";

            if(listeFabriquant.Count == 0)
            {
                cbReservationCreate_marque.DataSource = null;
                cbReservationCreate_model.DataSource = null;
                cbReservationCreate_noPlaque.DataSource = null;
                cbReservationCreate_noPlaque.ResetText();
                cbReservationCreate_noPlaque.SelectedIndex = -1;
                cbReservationCreate_marque.ResetText();
                cbReservationCreate_marque.SelectedIndex = -1;
            }
            else
            {
                cbReservationCreate_marque.SelectedIndex = 0;
            }
            

        }

        /// <summary>
        /// COMBOBOX Choix du fabriquant
        /// </summary>
        private void cbReservation_marque_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listeFabriquant.Count != 0)
            {

                int succID = (((KeyValuePair<int, string>)cbReservationCreate_Succursale.SelectedItem).Key);
                int fabricantID = (((KeyValuePair<int, string>)cbReservationCreate_marque.SelectedItem).Key);            

                listeDesModeles = new List<modele>();
                listeDesModeles = modelesServices.DistinctModelBySuccursaleAndFabricant(succID, fabricantID);

                // LISTE DES MODELES

                Dictionary<int, string> dictionaryModeles = new Dictionary<int, string>();

                    foreach (modele model in listeDesModeles)
                    {
                        dictionaryModeles.Add(model.modeleID, model.nom_modele);
                    }
            
                cbReservationCreate_model.DataSource = new BindingSource(dictionaryModeles, null);
                cbReservationCreate_model.DisplayMember = "Value";
                cbReservationCreate_model.ValueMember = "Key";

                if (listeDesModeles.Count == 0)
                {
                    cbReservationCreate_model.DataSource = null;
                    cbReservationCreate_model.ResetText();
                    cbReservationCreate_model.SelectedIndex = -1;
                }
                else
                {
                    cbReservationCreate_model.SelectedIndex = 0;
                }

            }
        }


        /// <summary>
        /// COMBOX Choix du modele
        /// </summary>        
        private void cbReservationCreate_model_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listeDesModeles.Count != 0)
            {
                cbReservationCreate_nbPassager.Items.Clear();

                int modeleID = (((KeyValuePair<int, string>)cbReservationCreate_model.SelectedItem).Key);
                cbReservationCreate_nbPassager.Items.Add(modelesServices.getNbPassager(modeleID));

                cbReservationCreate_nbPassager.SelectedIndex = 0;
            }
            else
            {
                cbReservationCreate_nbPassager.Items.Clear();
                cbReservationCreate_nbPassager.ResetText();
                cbReservationCreate_nbPassager.SelectedIndex = -1;
            }            

        }

        /// <summary>
        /// COMBOBOX nombre de passager
        /// </summary>
        private void cbReservationCreate_nbPassager_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listeDesModeles.Count != 0 && listeVehicule.Count != 0)
            {

                int modeleID = (((KeyValuePair<int, string>)cbReservationCreate_model.SelectedItem).Key);
                int fabricantID = (((KeyValuePair<int, string>)cbReservationCreate_marque.SelectedItem).Key);
                int succID = (((KeyValuePair<int, string>)cbReservationCreate_Succursale.SelectedItem).Key);

                //MessageBox.Show(String.Format("Model : {0} Fabricant : {1} Succ : {2}", modeleID.ToString(), fabricantID.ToString(), succID.ToString()));

                cbReservationCreate_noPlaque.DataSource = null;

                Dictionary<int, string> dictionaryVehicule = new Dictionary<int, string>();



                foreach (vehicule veh in listeVehicule)
                {                    

                    if (veh.modeleID == modeleID && veh.fabriquantID == fabricantID && veh.succursaleID == succID)
                    {
                        cbReservationCreate_noPlaque.Items.Add(veh.plaque_num);
                        dictionaryVehicule.Add(veh.vehiculeID, veh.plaque_num);
                    }
                }

                cbReservationCreate_noPlaque.DataSource = new BindingSource(dictionaryVehicule, null);
                cbReservationCreate_noPlaque.DisplayMember = "Value";
                cbReservationCreate_noPlaque.ValueMember = "Key";

                cbReservationCreate_noPlaque.SelectedIndex = 0;
            }
            else
            {
                cbReservationCreate_noPlaque.DataSource = null;
                cbReservationCreate_noPlaque.ResetText();
                cbReservationCreate_noPlaque.SelectedIndex = -1;
            }
        }

        private void btnReservationCreate_creerClient_Click(object sender, EventArgs e)
        {
            ClientForm clientForm = new ClientForm("Création");
            clientForm.Owner = this;
            clientForm.ShowDialog();

        }
    }
}
