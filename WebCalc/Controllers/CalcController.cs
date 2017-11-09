using CalcLibrary;
using DBModel;
using DBModel.Model;
using DBModel.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using WebCalc.Models;
using WinFormsCalculator;

namespace WebCalc.Controllers
{
    [Authorize]
    public class CalcController : Controller
    {
        private Calculator calc;

        private IList<Favorite> favorites
        {
            get
            {
                return DB.GetFavorites(HttpContext.User.Identity.Name);
            }
        }

        public CalcController()
        {
            calc = new Calculator();
            var extDir = HostingEnvironment.MapPath("~/App_Data");

            foreach (var op in CalcHelper.GetOperations(extDir))
            {
                calc.Operations.Add(op);
            }
        }

        // GET: Calc
        [HttpGet]
        public ActionResult Index(string operName = "")
        {
            var model = new OperViewModel();
            model.Favorites = favorites;
            
            ViewData.Model = model;
            if (string.IsNullOrWhiteSpace(operName))
                ViewBag.Operations = new SelectList(calc.Operations, "Name", "Name");
            else
                ViewBag.Operations = new SelectList(calc.Operations, "Name", "Name", operName);
            return View();
        }

        [HttpPost]
        public ActionResult Index(OperViewModel model)
        {
            model.Favorites = favorites;
            ViewBag.Operations = new SelectList(calc.Operations, "Name", "Name", model);
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Все пропало...";
                return View(model);
            }

            var oper = calc.Operations.FirstOrDefault(o => o.Name == model.OperName);
            if (oper != null)
            {
                model.Result = oper.Execute(CalcHelper.StringConverter(model.Args));
            }
            else
            {
                ViewBag.Error = "Operation not found";
            }
            
            return View(model);
        }

        public ActionResult Like(string operName)
        {
            if (favorites.Any(f => f.Name == operName))
                return Json(new { failed = true });

            var fav = new Favorite(operName);
            var currentUser = new UserManager().Get(HttpContext.User.Identity.Name);

            fav.Operation = new OperationManager().Get(operName);
            fav.User = currentUser;

            //сохранение операции в БД
            DB.AddFavorite(fav);
            //отображение кнопки
            return PartialView("FavButton", operName);
        }
    }
}