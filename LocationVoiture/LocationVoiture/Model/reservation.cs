//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LocationVoiture.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class reservation
    {
        public reservation()
        {
            this.constats_dommages = new HashSet<constats_dommages>();
            this.locations = new HashSet<location>();
        }
    
        public int reservationID { get; set; }
        public int clientID { get; set; }
        public int employeID { get; set; }
        public int vehiculeID { get; set; }
        public int succursaleID { get; set; }
        public Nullable<int> factureID { get; set; }
        public System.DateTime date_appel_reservation { get; set; }
        public Nullable<System.DateTime> date_debut_reservation { get; set; }
        public Nullable<System.DateTime> date_fin_reservation { get; set; }
        public Nullable<System.DateTime> date_prise_vehicule { get; set; }
        public Nullable<System.DateTime> date_retour_vehicule { get; set; }
    
        public virtual client client { get; set; }
        public virtual ICollection<constats_dommages> constats_dommages { get; set; }
        public virtual employe employe { get; set; }
        public virtual succursale succursale { get; set; }
        public virtual vehicule vehicule { get; set; }
        public virtual ICollection<location> locations { get; set; }
    }
}