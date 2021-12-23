using BenivoNetwork.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BenivoNetwork.Models
{
    public class HomeModel
    {
        public int Count { get; set; }
        [Required(ErrorMessage = "Պարտադիր դաշտ")]
        [MinLength(2, ErrorMessage = "Քիչ է")]
        [MaxLength(10)]
        public string Text { get; set; }
        public bool IsImported { get; set; }
        public TestEnum DayType { get; set; }
        public List<string> Names { get; set; }
    }
}