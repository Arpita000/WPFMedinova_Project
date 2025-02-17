using System.ComponentModel.DataAnnotations;

namespace WPFMedinova.Models
{
    public class Specialization
    {
        [Key]
        public int S_Id { get; set; }
        public string ?S_Name { get; set; }
    }
}
