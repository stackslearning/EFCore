using Microsoft.EntityFrameworkCore;
using Samurai.Domain;

namespace Samurai.Data
{
	public class SamuraiContext : DbContext
	{
		public DbSet<Domain.Samurai> Samurais { get; set; }
		public DbSet<Battle> Battles { get; set; }
		public DbSet<Quote> Quotes { get; set; }
	}
}
