using LocationVoiture.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationVoiture.Services
{
    class SuccursalesServices
    {
        public locationvoitureEntities succursaleEntitie { get; private set; }

        public SuccursalesServices()
        {
            succursaleEntitie = new locationvoitureEntities();
        }



        public void addClient(client client)
        {
            // TODO          
        }


        public void find(String searchValue, String searchBy)
        {

        }


        public List<succursale> getAllSuccursale()
        {
            List<succursale> listSsuccursale = new List<succursale>();
            try
            {
                listSsuccursale = succursaleEntitie.succursales.ToList();
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                Console.WriteLine("Erreur : Cannot retreive the fabriquants list");
            }

            return listSsuccursale;
        }

        /// <summary>
        /// Enregistre les modification fait à la table client
        /// </summary>
        public void Save()
        {
            succursaleEntitie.SaveChanges();
        }
    }
}
