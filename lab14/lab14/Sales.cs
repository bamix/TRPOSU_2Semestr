using System.ComponentModel.DataAnnotations.Schema;

namespace lab14
{
    class Sales
    {
        public int Id { get; set; }

        public virtual Good Good { get; set; }

        public int Count { get; set; }

        public string CustomerName { get; set; }

        [NotMapped]
        public string GoodName => Good?.Name;
    }
}
