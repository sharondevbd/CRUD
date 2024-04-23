using Microsoft.EntityFrameworkCore;

namespace CodeFirstMasterDetails.Models
{
	public class ApplicantContext:DbContext
	{
		public ApplicantContext(DbContextOptions<ApplicantContext> options) : base(options)
		{
		}
		//protected override void OnConfiguring(DbContextOptionsBuilder options)
		//{
		//	options.UseSqlServer("DefaultConnection");
		//}
		public DbSet<Applicant> Applicants { get; set; }
		public DbSet<ApplicantExprience> Expriences { get; set; }

	}
}
