using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace lab14
{
    class Resource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Good> Goods { get; set; }

        [NotMapped]
        public string GoodsList => string.Join(", ", (Goods ?? new List<Good>()).Select(r => r.Name).ToList());
    }
}
