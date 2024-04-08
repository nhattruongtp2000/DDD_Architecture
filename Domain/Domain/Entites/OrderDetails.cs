using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class OrderDetails
    {
        [Key]
        public string OrderDetailId { get; set; }

        public string OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quality { get; set; }

        public decimal Price { get; set; }

        public string StatusDetails { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

    }
}
