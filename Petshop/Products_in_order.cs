//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Petshop
{
    using System;
    using System.Collections.Generic;
    
    public partial class Products_in_order
    {
        public int id_product { get; set; }
        public int id_order { get; set; }
        public Nullable<int> count_of_products { get; set; }
        
        public decimal TotalPrice 
        { 
            get
            {
                return (decimal)(Products.price * count_of_products);
            }
        }

        public virtual Orders Orders { get; set; }
        public virtual Products Products { get; set; }
    }
}