using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace MvcProduct.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Display(Name = "Дата заказа")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Display(Name = "Статус")]
        public string Status { get; set; } = "Новый";
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        [Display(Name = "Клиент")]
        public virtual Customer Customer { get; set; }
    }
}