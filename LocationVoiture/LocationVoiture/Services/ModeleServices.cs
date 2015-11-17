using LocationVoiture.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationVoiture.Services
{
    class ModeleServices
    {
        public locationvoitureEntities modeleEntitie { get; private set; }

        public ModeleServices()
        {
            modeleEntitie = new locationvoitureEntities();
        }

        public List<modele> DistinctModelBySuccursaleAndFabricant(int succursaleID, int fabricantID)
        {
            List<modele> listeDistinctModels = new List<modele>();

            var query = (from vehi in modeleEntitie.vehicules.Include("modele").Include("fabriquant")
                         where vehi.succursaleID == succursaleID
                         where vehi.fabriquantID == fabricantID
                         select vehi.modele).Distinct();

            listeDistinctModels = query.ToList();

            return listeDistinctModels;
        }

        public int getNbPassager(int modeleID)
        {
            int nbPassager = 0;

            modele mod = modeleEntitie.modeles.Where(p => p.modeleID == modeleID).Single();

            nbPassager = (int) mod.nb_place;

            return nbPassager;
        }

    }
}
