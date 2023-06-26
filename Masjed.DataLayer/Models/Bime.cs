using Masjed.Utilites.Convertor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masjed.DomainClass
{
    public class Bime
    {
        public Bime()
        {

            date = DateTime.Now.ToShortDateString().ConvertToShamsi();

        }
        [Key]
        public int Id { get; set; }
        //____________________________________________________________________
        [Display(Name = "نام بیمه")]
        public string Title { get; set; }
        //____________________________________________________________________
        [Display(Name = "هزینه بیمه")]
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        public int PriceBime { get; set; }
        //____________________________________________________________________
        [Display(Name = "ازتاریخ")]
        public string AsDate { get; set; }
        //____________________________________________________________________
        [Display(Name = "تاتاریخ")]
        public string TaDate { get; set; }
        //___________________________________________________________________
        public string date { get; set; }
    }
}
