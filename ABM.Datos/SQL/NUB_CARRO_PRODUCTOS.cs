//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ABM.Datos.SQL
{
    using System;
    using System.Collections.Generic;
    
    public partial class NUB_CARRO_PRODUCTOS
    {
        public int carpord_id { get; set; }
        public int pro_id { get; set; }
        public int car_id { get; set; }
        public int pro_monto { get; set; }
        public int carpord_cantidad { get; set; }
    
        public virtual TBL_CARRO_COMPRA TBL_CARRO_COMPRA { get; set; }
        public virtual TBL_PRODUCTO TBL_PRODUCTO { get; set; }
    }
}