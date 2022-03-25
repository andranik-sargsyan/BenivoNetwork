using System.ComponentModel.DataAnnotations;

namespace BenivoNetwork.Common.Models
{
    public abstract class TextFormModel
    {
        public int ID { get; set; }
        [Required]
        public string Text { get; set; }

        public string HtmlText => Text
            .Replace("\n", "<br />")
            .Replace(":)", "🙂")
            .Replace(":(", "😞")
            .Replace(":D", "😂")
            .Replace(":P", "😛");
    }
}
