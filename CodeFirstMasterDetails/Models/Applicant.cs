using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstMasterDetails.Models
{
	public class Applicant
	{
		public Applicant() {
			Exprience = new List<ApplicantExprience>();
		}
		public int Id { get; set; }
		public string Name { get; set; }
		[DataType(DataType.Date)]
		public DateTime Birthday { get; set; }
		public int TotalExp { get; set; }
		[ValidateNever]
		public string PicPath { get; set; } = null;
		[NotMapped]
		public IFormFile Picture { get; set; } = null;
		public bool IsAvilable { get; set; }
		public List<ApplicantExprience> Exprience { get;set; }
	}
}
