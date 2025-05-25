using System.ComponentModel.DataAnnotations;

namespace WebApplication6.Models
{
    public class Category
    {
        public int  Id { get; set; }

        [Required(ErrorMessage ="لطفا نام را وارد کنید")]
        [Display(Name="نام دسته بندی")]
        public string Name { get; set; }


        public ICollection<Product> Products { get; set; }
    }
}
