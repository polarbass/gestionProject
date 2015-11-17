using LocationVoiture.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocationVoiture.Services
{
    class LoginServices
    {
        public locationvoitureEntities LoginEntitie { get; private set; }

        public LoginServices()
        {
            LoginEntitie = new locationvoitureEntities();
        }


        public void addEmploye(administrateur administrateur)
        {
             
        }

        /// <summary>
        /// Recherche un client dans la base de donné
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public administrateur find(String searchValue, String searchBy)
        {
            administrateur administratorFinder = new administrateur();

            administratorFinder = LoginEntitie.administrateurs.SingleOrDefault(p => p.nom == searchValue);

            return administratorFinder;

        }

        public Boolean loginAccepted(String username, String password)
        {
            bool isAccepted = false;

            administrateur adminChecker = find(username, "nom");

            if(adminChecker != null)
            {
                if(adminChecker.nom.Equals(username) && adminChecker.password.Equals(password))
                {
                    isAccepted = true;
                }
            }

            return isAccepted;
        }

        /// <summary>
        /// Retourne une liste qui contient tout les clients enregistré dans la table client
        /// </summary>
        /// <returns></returns>
        public List<administrateur> getAllAdministrators()
        {
            List<administrateur> listeAdministrateurs = new List<administrateur>();
            try
            {
                listeAdministrateurs = LoginEntitie.administrateurs.ToList();
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                Console.WriteLine("Erreur : Cannot retreive the admin list");
            }
            
            return listeAdministrateurs;
        }

        /// <summary>
        /// Enregistre les modification fait à la table client
        /// </summary>
        public void Save()
        {
            LoginEntitie.SaveChanges();
        }
    }
}
