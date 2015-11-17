using LocationVoiture.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocationVoiture.Services
{
    class ReservationsServices
    {
        public locationvoitureEntities reservationEntitie { get; private set; }

        public ReservationsServices()
        {
            reservationEntitie = new locationvoitureEntities();
        }

        public Boolean AddReservation(reservation reservationToAdd)
        {
            bool isAdded = false;



            try
            {
                reservationEntitie.reservations.Add(reservationToAdd);
                reservationEntitie.SaveChanges();
                MessageBox.Show("reservation added");
                isAdded = true;
            }
            catch
            {

                MessageBox.Show("erreur !!!!");

            }
            return isAdded;
        }
    }
}
