using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVC.Models.utility
{
    public class MyFilterFactory :Attribute, IFilterFactory
    {
        private readonly Type _filterType = null;
        public MyFilterFactory(Type type) {
            _filterType = type;
        }
        public bool IsReusable => true;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
           return   (IFilterMetadata)serviceProvider.GetService(this._filterType);
        }
    }

}
