using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVC.Controllers
{
    public class UsersController : Controller
    {
        public UsersController() { 
        
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
