using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDetails.Models
{
	public class Category_Tbl
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
		//public int CategoryID { get; set; }
		public string CategoryName { get; set; }
		public string CategoryDescription { get; set; }
		public DateTime CreatedDate { get; set; }
		public int CreatedBy { get; set; }
		public int MaterialID  { get; set; }
        public int StatusID { get; set; }
		public int DeletedBy { get; set; }
		public string? keyEntry1  { get; set; }
        public string? keyEntry2  { get; set; }
        public string? keyEntry3  { get; set; }


    }
}

