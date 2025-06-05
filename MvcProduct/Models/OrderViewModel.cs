using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MvcProduct.Models;

namespace MvcProduct.Models
{
    public class OrderViewModel
    {
        public List<Order>? Orders { get; set; }
        public SelectList? Statuses { get; set; }
        public string? OrderStatus { get; set; }
        public string? CustomerSearch { get; set; }
        public string? ProductSearch { get; set; }
    }
}