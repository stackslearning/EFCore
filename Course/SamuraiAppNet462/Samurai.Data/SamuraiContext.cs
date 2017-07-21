using Microsoft.EntityFrameworkCore;
using Samurai.Domain;

namespace Samurai.Data
{
	public class SamuraiContext : DbContext
	{
		public DbSet<Domain.Samurai> Samurais { get; set; }
		public DbSet<Battle> Battles { get; set; }
		public DbSet<Quote> Quotes { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var cnn = "Server=(localdb)\\mssqllocaldb;Database=SamuraiData;Trusted_Connection=True";
			optionsBuilder.UseSqlServer(cnn);

			optionsBuilder.EnableSensitiveDataLogging();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Note: Instead of specifying data annotations on the domain model classes (and pollute them with many attributes)
			//to identify the various changes we can do like saying that a table column has a different name than the class property
			//or specifying the key using the attribute [Key] on the class property itself,
			//it's quite handy to apply and visualize 'all' these annotations/qualifications/restrictions here only in one place
			//and even taking advantage of the Fluent Api via DbContext!

			//Specifying more than one property/column as the primary key (Fluent Api).
			//Seems that this cannot be done using the data annotation syntax by the time this was coded, anyways.
			modelBuilder
				.Entity<SamuraiBattle>()
				.ToTable("SamuraisBattles")
				.HasKey(sb => new { sb.SamuraiId, sb.BattleId });

			//In case we want to force a Samurai have a SecretIdentity.
			//modelBuilder
			//	.Entity<Domain.Samurai>()
			//	.Property(s => s.SecretIdentity)
			//	.IsRequired();

			modelBuilder
				.Entity<SecretIdentity>()
				.Property(si => si.RealName)
				.IsRequired();

			base.OnModelCreating(modelBuilder);
		}
	}
}
