using DBModel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCalc.Models
{
    public class OperViewModel
    {
        public string OperName { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string Args { get; set; }
        public double? Result { get; set; }

        public IList<Favorite> Favorites { get; set; }
    }
}