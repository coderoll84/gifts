﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Mvc.Data.Models
{
    public partial class Producto
    {
        public Producto()
        {
            ProductoOcasiones = new HashSet<ProductoOcasione>();
        }

        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public int IdCategoria { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; }
        public virtual Proveedor IdProveedorNavigation { get; set; }
        public virtual ICollection<ProductoOcasione> ProductoOcasiones { get; set; }
    }
}