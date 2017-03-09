using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Medication
    {
        public Medication()
        {
            Id = null;
            Name = null;
            Desc = null;
        }

        [DisplayName("NDC UPC HRI")]
        public int? Id { get; set; }
        [DisplayName("GPI Generic Name")]
        public string Name { get; set; }
        [DisplayName("Description")]
        public string Desc { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Desc)}: {Desc}";
        }
    }
}