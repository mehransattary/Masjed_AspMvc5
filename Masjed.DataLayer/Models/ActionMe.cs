using Masjed.Utilites.Convertor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Masjed.DomainClass
{
    public class ActionMe
    {
        public ActionMe()
        {
            date = DateTime.Now.ToShortDateString().ConvertToShamsi();
        }
        [Key]
        public int Id { get; set; }
        //____________________________________________________________________
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        [MaxLength(200, ErrorMessage = ".بیشتر از{0}کاراکترواردنشود")]
        public string Title { get; set; }
        //____________________________________________________________________
        [Display(Name = "تاریخ فعالیت")]
        public string DateCreate { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        //____________________________________________________________________

        //____________________________________________________________________
        [Display(Name = "تصویر اصلی فعالیت")]
        public string Img { get; set; }
        //____________________________________________________________________
        [Display(Name = "توضیحات")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }
        //____________________________________________________________________
        public string date { get; set; }
    }
}
