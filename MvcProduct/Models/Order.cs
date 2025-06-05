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

        [Display(Name = "���� ������")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Display(Name = "������")]
        public string Status { get; set; } = "�����";
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        [Display(Name = "������")]
        public virtual Customer Customer { get; set; }
    }
}