using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using MvcProduct.Models;

namespace MvcProduct.Models
{
    public class CustomerViewModel
    {
        public List<Customer>? Customers { get; set; }
        public string? SearchFirstName { get; set; }
        public string? SearchLastName { get; set; }
        public string? SearchCity { get; set; }
        public SelectList? Cities { get; set; }
    }
}