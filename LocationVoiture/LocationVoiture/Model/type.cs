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
    
    public partial class type
    {
        public type()
        {
            this.modeles = new HashSet<modele>();
        }
    
        public int typeID { get; set; }
        public string nom_type { get; set; }
        public string description { get; set; }
        public float commission { get; set; }
        public string catégorie { get; set; }
    
        public virtual ICollection<modele> modeles { get; set; }
    }
}
