using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KooliProjekt.Application.Data
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
