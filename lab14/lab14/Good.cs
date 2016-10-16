using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace lab14
{
    class Good
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Resource> Resources { get; set; }
        public virtual List<Sales> Saleses { get; set; }

        [NotMapped]
        public string ResourcesList => string.Join(", ", (Resources ?? new List<Resource>()).Select(r => r.Name).ToList());
    }
}
