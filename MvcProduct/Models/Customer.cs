using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace MvcProduct.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Фамилия")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Телефон")]
        public string Phone { get; set; } = string.Empty;

        [Display(Name = "Город")]
        [StringLength(50, MinimumLength = 2)]
        public string City { get; set; }  
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}