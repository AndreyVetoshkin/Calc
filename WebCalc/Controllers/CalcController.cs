using CalcLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCalc.Models;
using WinFormsCalculator;

namespace WebCalc.Controllers
{
    public class CalcController : Controller
    {
        Calculator calc = new Calculator();
        IEnumerable<IOperation> opers = CalcHelper.GetOperations();
        // GET: Calc
        [HttpGet]
        public ActionResult Index()
        {
            if (opers != null)
                foreach (var item in opers)
                    calc.Operations.Add(item);
            ViewData.Model = new OperViewModel();
            ViewBag.Operations = new SelectList(calc.Operations, "Name", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Index(OperViewModel model)
        {
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
            ViewBag.Operations = new SelectList(calc.Operations, "Name", "Name");
            return View(model);
        }
    }
}