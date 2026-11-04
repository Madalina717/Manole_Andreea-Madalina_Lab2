using System.ComponentModel.DataAnnotations;

namespace Manole_Andreea_Madalina_Lab2.Models
{
    public class Borrowing
    {
        public int ID { get; set; }

        public int? MemberID { get; set; }
        public Member? Member { get; set; }

        public int? BookID { get; set; }
        public Book? Book { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }
    }
}
