﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Mvc.Data.Models
{
    public partial class ProductoOcasione
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdOcasion { get; set; }

        public virtual Ocasione IdOcasionNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
    }
}