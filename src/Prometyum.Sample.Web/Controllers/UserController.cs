using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Prometyum.Sample.Web.Controllers
{
    public class UserController : Controller
    {
        public List<SelectListItem> Search(string q)
        {
            return new List<SelectListItem>
            {
                new SelectListItem("Müslüm","1"),
                new SelectListItem("Akif","2"),
                new SelectListItem("Şehri","3"),
            };
        }

    }
}
