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
        IEnumerable<IOperation> opers;
        public CalcController()
        {
            calc = new Calculator();
            var extDir = HostingEnvironment.MapPath("~/App_Data");
            opers = CalcHelper.GetOperations(extDir);

            favorites = DB.GetFavorites();
        }

        // GET: Calc
        [HttpGet]
        public ActionResult Index(string operName = "")
        {
            var model = new OperViewModel();
            model.Favorites = favorites;
            if (opers != null)
                foreach (var item in opers)
                    calc.Operations.Add(item);
            ViewData.Model = model;
            if (string.IsNullOrWhiteSpace(operName))
                ViewBag.Operations = new SelectList(calc.Operations, "Name", "Name");
            else
                ViewBag.Operations = new SelectList(calc.Operations, "Name", "Name", calc.Operations.FirstOrDefault(o => o.Name == operName));
            return View();
        }

        [HttpPost]
        public ActionResult Index(OperViewModel model)
        {
            model.Favorites = favorites;

            if (opers != null)
                foreach (var item in opers)
                    calc.Operations.Add(item);
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
            return PartialView("Like", operName);
        }

        public ActionResult FavRedir(string _operName)
        {
            return RedirectToAction("Index", "Calc", new { operName = _operName });
        }
    }
}