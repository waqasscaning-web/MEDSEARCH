using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OMS.Models
{
    public class MadicalStore
    {
        public int MadicalStoreID { get; set; }
        public string Name { get; set; }

        [Required]
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        [Required]
        [Display(Name = "Store Name")]

        public string StoreName { get; set; }
        [Required]
        [Display(Name = "Store License")]
        public string StoreLicense { get; set; }
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        [Required]
        [Display(Name = "LandLine ")]
        public string LandLine { get; set; }
        [Required]
        [Display(Name = "Store Address ")]
        public string StoreAddress { get; set; }
        public string userid { get; set; }
        public string StoreImage { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}