using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OMS.Models
{
    public class Products
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string ProducName { get; set; }
        [Required]
        [Display(Name = "Product Formula")]
        public string Producformula { get; set; }
        [Required]
        [Display(Name = "Product Weight")]
        public string Productweight { get; set; }

        [Required]
        [Display(Name = "Product Price")]
        public decimal Price { get; set; }


        [Required]
        [Display(Name = "Product Discrioption")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Product Category")]
        public string Category { get; set; }
        public string productImage { get; set; }
        public int? MadicalStoreID { get; set; }
        public virtual MadicalStore MadicalStore { get; set; }
    }
}