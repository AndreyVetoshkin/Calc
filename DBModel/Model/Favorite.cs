using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.Model
{
    public class Favorite
    {
        public Favorite()
        {
        }

        public Favorite(string name)
        {
            Name = name;
            CreationDate = DateTime.Now;
        }

        public Favorite(string name, bool isCustom)
        {
            Name = name;
            CreationDate = DateTime.Now;
            IsCustom = isCustom;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsCustom { get; set; }
    }
}
