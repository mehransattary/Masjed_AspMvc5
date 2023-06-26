using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masjed.DomainClass
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public int ActionId { get; set; }
        //____________________________________________________________________
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        [Display(Name = "نام")]
        [MaxLength(100, ErrorMessage = ".بیشتر از{0}کاراکترواردنشود")]
        public string Fname { get; set; }
        //____________________________________________________________________
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        [Display(Name = " نام خانوادگی")]
        [MaxLength(100, ErrorMessage = ".بیشتر از{0}کاراکترواردنشود")]
        public string Lname { get; set; }
        //____________________________________________________________________
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        [Display(Name = "سن")]
        public int Age { get; set; }
        //____________________________________________________________________
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        [Display(Name = "کدملی")]
        public int CodeMeli { get; set; }
        //____________________________________________________________________

        [Display(Name = "بیمه")]
        public int BimeId { get; set; }
        //____________________________________________________________________      
        [Display(Name = "بیمه شده")]
        public bool IsBime { get; set; }
        //____________________________________________________________________
        [ForeignKey("BimeId")]
        public virtual Bime Bime { get; set; }
        public virtual ActionMe ActionMe { get; set; }
    }
}
