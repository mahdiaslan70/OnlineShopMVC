using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication6.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [DisplayName("نام")]
        public string Name { get; set; }



        [DisplayName("توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }



        [DisplayName("آدرس تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        
        public string ImageUrl { get; set; }



        [DisplayName("قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public decimal Price { get; set; }



        [DisplayName("موجودی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Stock { get; set; }



        [DisplayName("تاریخ ایجاد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime CreatedAt { get; set; }


        [DisplayName("شماره دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CategoryId { get; set; }


        [ValidateNever]
        public Category Category { get; set; }
            

        [ValidateNever]
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
