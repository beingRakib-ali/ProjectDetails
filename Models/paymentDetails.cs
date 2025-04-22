using System.ComponentModel.DataAnnotations;

namespace ProjectDetails.Models
{
    public class paymentDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string ProjectName{ get; set; }
        [Required]

        public DateTime Date { get; set; }

        public string Description { get; set; }

        [Required]
        public string TotalCost { get; set; }
        [Required]
        public string ShahadotAmount { get; set; }
        [Required]
        public string JonyAmount { get; set; }
        [Required]
        public string ShahadotStatus { get; set; }
        [Required]
        public string JonyStatus { get; set; }
        [Required]
        public string PaidAmountPercentage { get; set; }
        public int Status { get; set; }
       

    }
}


//Project Name	Date	Total Cost	Shahadot's Amount	Jony's Amount	Shahadot's Status	Jony's Status	Actions