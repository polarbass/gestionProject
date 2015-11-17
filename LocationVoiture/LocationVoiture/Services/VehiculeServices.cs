using LocationVoiture.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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

        public List<vehicule> getVehiculeFromSuccursale(int succuraleID)
        {

            List<vehicule> vehiculeFinder = new List<vehicule>();

            var query = from vehi in vehiculeEntitie.vehicules.Include("modele").Include("fabriquant")
                        where vehi.succursaleID == succuraleID
                        select vehi;

            vehiculeFinder = query.ToList();

            return vehiculeFinder;

        }

        public List<fabriquant> getDistinctFabriquant(int succuraleID)
        {
            List<fabriquant> vehiculeFinder = new List<fabriquant>();

            var query = (from vehi in vehiculeEntitie.vehicules.Include("modele").Include("fabriquant")
                        where vehi.succursaleID == succuraleID
                        select vehi.fabriquant).Distinct();

            vehiculeFinder = query.ToList();

            return vehiculeFinder;
        }

        public List<modele> getDistinctModele(int succuraleID)
        {
            List<modele> vehiculeFinder = new List<modele>();

            var query = (from vehi in vehiculeEntitie.vehicules.Include("modele").Include("fabriquant")
                         where vehi.succursaleID == succuraleID
                         select vehi.modele).Distinct();

            vehiculeFinder = query.ToList();

            return vehiculeFinder;
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
