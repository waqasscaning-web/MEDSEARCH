using OMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Web;

namespace OMS.ViewModel
{
    public class ProductStoreViewModel
    {
        public IEnumerable<Products> products { get; set; }
        public IEnumerable<MadicalStore> madicalStores { get; set; }
    }
}