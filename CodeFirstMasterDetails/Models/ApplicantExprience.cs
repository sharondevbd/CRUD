using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstMasterDetails.Models
{
	public class ApplicantExprience
	{
		public int Id { get; set; }
		public string Company { get; set; }
		public string Designation { get; set; }
		public int YearOfExp { get; set; }
		[ForeignKey("Applicant")]
		public int ApplicantId { get; set; }
		public virtual Applicant Applicant { get; set; }
	}
}
