using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcProduct.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "��������")]
        [StringLength(100, MinimumLength = 3)]
        public string? Name { get; set; }

        [Display(Name = "���� ������������")]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        [Display(Name = "���������")]
        [RegularExpression(@"^[A-Z�-�]+[a-zA-Z�-��-�0-9""'\s-]*$")]
        [Required]
        [StringLength(30)]
        public string? Category { get; set; }

        [Display(Name = "����")]
        [Range(100, 1000000)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Display(Name = "����� �����������������")]
        [RegularExpression(@"^[A-Z�-�]+[a-zA-Z�-��-�0-9""'\s+-]*$")]
        [StringLength(10)]
        public string? EnergyClass { get; set; }

        [Display(Name = "� �������")]
        public int StockQuantity { get; set; }

        [Display(Name = "��������")]
        [StringLength(500)]
        public string? Description { get; set; }

        [Display(Name = "�������")]
        [Range(1, 5)]
        [Column(TypeName = "decimal(3, 1)")]
        public decimal Rating { get; set; }

        [Display(Name = "�������� (���.)")]
        [Range(0, 120)]
        public int WarrantyMonths { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

}
