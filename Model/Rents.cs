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
    
    public partial class Rents
    {
        public int Rent_Id { get; set; }
        public Nullable<int> Tenant_Id { get; set; }
        public Nullable<int> ShopCenter_Id { get; set; }
        public Nullable<int> Employee_Id { get; set; }
        public string Pavilion_Number { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> BeginRent { get; set; }
        public Nullable<System.DateTime> EndRent { get; set; }
    
        public virtual Employees Employees { get; set; }
        public virtual Pavilions Pavilions { get; set; }
        public virtual Tenants Tenants { get; set; }
    }
}