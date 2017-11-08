using CalcLibrary;
using DBModel;
using DBModel.Model;
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
    public class CalcController : Controller
    {
        private Calculator calc;
        private IList<Favorite> favorites { get; set; }      
        public CalcController()
        {
            calc = new Calculator();
            var extDir = HostingEnvironment.MapPath("~/App_Data");

            foreach (var op in CalcHelper.GetOperations(extDir))
            {
                calc.Operations.Add(op);
            }

            favorites = DB.GetFavorites();
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
            ViewBag.Operations = new SelectList(calc.Operations, "Name", "Name", model);
            return View(model);
        }

        public ActionResult Like(string operName)
        {
            if (favorites.Any(f => f.Name == operName))
                return Json(new { failed = true });
            //сохранение операции в БД
            DB.AddFavorite(new Favorite(operName));
            //отображение кнопки
            return PartialView("FavButton", operName);
        }
    }
}