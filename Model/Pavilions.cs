//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KingIT.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pavilions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pavilions()
        {
            this.Rents = new HashSet<Rents>();
        }
    
        public string Pavilion_Number { get; set; }
        public int ShopCenter_Id { get; set; }
        public Nullable<int> Floor { get; set; }
        public string Status { get; set; }
        public Nullable<double> Square { get; set; }
        public Nullable<double> CostPerSq_m { get; set; }
        public Nullable<double> Cof_Added_value { get; set; }
    
        public virtual ShopCentres ShopCentres { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rents> Rents { get; set; }
    }
}