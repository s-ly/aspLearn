using System.ComponentModel.DataAnnotations;

namespace Calc.Models
{
    public class CalcData
    {
        [Required(ErrorMessage ="Ввелите имя")]
        public string Str { get; set; }
        [Required(ErrorMessage ="Ввелите X")]
        public int X { get; set; }
        [Required(ErrorMessage ="Ввелите Y")]
        public int Y { get; set; }
        [Required(ErrorMessage ="Выберите действие")]
        public string Action { get; set; }

        public int Answer { get; set; }
    }
}