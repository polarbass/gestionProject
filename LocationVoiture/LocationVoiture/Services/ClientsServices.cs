using LocationVoiture.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocationVoiture.Services
{
    class ClientsServices
    {
        public locationvoitureEntities ClientsEntitie { get; private set; }

        public ClientsServices()
        {
            ClientsEntitie = new locationvoitureEntities();
        }


        /// <summary>
        /// Ajouter un client dans la table Client
        /// </summary>
        /// <param name="client">Le client à ajouter</param>
        /// <returns>True si le client à été ajouté ; False sinon</returns>
        public Boolean addClient(client client)
        {

            bool isAdded = false;

            // valeur par défaut
            client.permis_conduire_num = "0000000000";
            client.assurance = "0000000000";
            client.date_enregistrement = DateTime.Now;

            // mot de passe temporaire (prenom + nom + jour de l'inscription)
            client.password = client.prenom.ToLower() + client.nom.ToLower() + DateTime.Now.Day;

            try
            {
                ClientsEntitie.clients.Add(client);
                ClientsEntitie.SaveChanges();
                MessageBox.Show("client added");
                isAdded = true;
            }
            catch
            {
                MessageBox.Show("Le client n'a pas pu etre ajoute");
            }

            return isAdded;
             
        }

        /// <summary>
        /// Recherche un client dans la base de donné
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<client> find(String searchValue, String searchBy)
        {
            List<client> clientFinder = new List<client>();

            if(searchValue != "")
            {

                try
                {
                
                // SEARCH BY CLIENT ID
                if (searchBy.Equals("client id"))
                {
                    int searchValueInt = int.Parse(searchValue);

                    var query = from c in ClientsEntitie.clients
                                where c.clientID == searchValueInt
                                select c;

                    clientFinder = query.ToList();
                }

                // SEARCH BY CLIENT NOM
                else if (searchBy.Equals("nom"))
                {
                    var query = from c in ClientsEntitie.clients
                                where c.nom.Equals(searchValue)
                                select c;

                    clientFinder = query.ToList();
                }

                // SEARCH BY CLIENT PRENOM
                else if (searchBy.Equals("prenom"))
                {
                    var query = from c in ClientsEntitie.clients
                                where c.prenom.Equals(searchValue)
                                select c;

                    clientFinder = query.ToList();
                }

                // SEARCH BY CLIENT PRENOM
                else if (searchBy.Equals("courriel"))
                {
                    var query = from c in ClientsEntitie.clients
                                where c.courriel.Equals(searchValue)
                                select c;

                    clientFinder = query.ToList();
                }

                // SEARCH BY CLIENT PRENOM
                else if (searchBy.Equals("telephone"))
                {
                    var query = from c in ClientsEntitie.clients
                                where c.telephone.Equals(searchValue)
                                select c;

                    clientFinder = query.ToList();
                }

                }
                catch
                {
                    Console.WriteLine("Erreur dans le find du client");
                }

            }

            return clientFinder;
        }

        /// <summary>
        /// Retourne une liste qui contient tout les clients enregistré dans la table client
        /// </summary>
        /// <returns></returns>
        public List<client> getAllClients()
        {
            List<client> listeClients = new List<client>();
            try
            {
                listeClients = ClientsEntitie.clients.ToList();
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                Console.WriteLine("Erreur : Cannot retreive the clients list");
            }
            
            return listeClients;
        }

        /// <summary>
        /// Enregistre les modification fait à la table client
        /// </summary>
        public void Save()
        {
            ClientsEntitie.SaveChanges();
        }
    }
}
