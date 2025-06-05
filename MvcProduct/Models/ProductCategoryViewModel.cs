using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcProduct.Models
{
    public class ProductCategoryViewModel
    {
        public List<Product>? Products { get; set; }
        public SelectList? Categories { get; set; }
        public string? ProductCategory { get; set; }
        public string? SearchString { get; set; }
        public decimal? MinRating { get; set; }
        public decimal? MaxRating { get; set; }
    }
}