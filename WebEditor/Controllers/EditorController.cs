using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebEditor.Controllers
{
    public class EditorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
