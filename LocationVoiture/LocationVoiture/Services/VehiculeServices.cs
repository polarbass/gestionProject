using LocationVoiture.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocationVoiture.Services
{
    class VehiculeServices
    {
        public locationvoitureEntities vehiculeEntitie { get; private set; }

        public VehiculeServices()
        {
            vehiculeEntitie = new locationvoitureEntities();
        }



        public void addClient(client client)
        {
             
        }


        public void find(String searchValue, String searchBy)
        {

        }


        public List<fabriquant> getAllFabriquant()
        {
            List<fabriquant> listeFabriquants = new List<fabriquant>();
            try
            {
                listeFabriquants = vehiculeEntitie.fabriquants.ToList();
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                Console.WriteLine("Erreur : Cannot retreive the fabriquants list");
            }
            
            return listeFabriquants;
        }

        /// <summary>
        /// Enregistre les modification fait à la table client
        /// </summary>
        public void Save()
        {
            vehiculeEntitie.SaveChanges();
        }
    }
}
