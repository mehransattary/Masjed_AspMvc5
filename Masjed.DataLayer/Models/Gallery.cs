using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masjed.DomainClass
{
    public class Gallery
    {
        [Key]
        public int Id { get; set; }
        //____________________________________________________________________
        [Display(Name = " تصویراصلی")]
        public string ImgMain { get; set; }
        //____________________________________________________________________

        [Display(Name = " تصویر اندازه کوچک")]
        public string ImgLetter { get; set; }
        //____________________________________________________________________
        public int ActionId { get; set; }
        //____________________________________________________________________
        [ForeignKey("ActionId")]
        public virtual ActionMe ActionMe { get; set; }
    }
}
