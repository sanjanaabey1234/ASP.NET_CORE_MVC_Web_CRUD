using Microsoft.EntityFrameworkCore;
using Web_CRUD.Models;

namespace Web_CRUD.Context
{
	public class ApplicationDbContext :DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext>ContextOptions)
			:base(ContextOptions)
		{

		}

		//Code - Approach
		//model eke class eken table structure eka 
		// ee kiyanne <Employee> model class eken table structure DbSet eka ta aran ekapaara Employees variable ekata danawa
		public DbSet<Employee> Employees { get; set; }

		//Models walin model class ekakka name eka denna <> methanata


	}
}
