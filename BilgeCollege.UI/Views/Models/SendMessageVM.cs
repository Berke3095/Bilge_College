using System.ComponentModel.DataAnnotations;

namespace BilgeCollege.UI.Views.Models
{
    public class SendMessageVM
    {
        [Required(ErrorMessage = "You must enter the email of the recieving user.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must write a message.")]
        public string Text { get; set; }
    }
}
