﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Mvc.Data.Models
{
    public partial class CodigosPostale
    {
        public CodigosPostale()
        {
            Direcciones = new HashSet<Direccione>();
        }

        public int Id { get; set; }
        public int IdMunicipio { get; set; }
        public int CodigoPostal { get; set; }
        public string Asentamiento { get; set; }

        public virtual Municipio IdMunicipioNavigation { get; set; }
        public virtual ICollection<Direccione> Direcciones { get; set; }
    }
}