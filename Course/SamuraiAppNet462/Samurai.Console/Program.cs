using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Samurai.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using SamuraiDomain = Samurai.Domain.Samurai;

namespace Samurai.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			Insert();
			Select();
		}

		private static void Insert()
		{
			InsertSamurai();
			void InsertSamurai()
			{
				var sam1 = new SamuraiDomain { Name = "Sam " + Guid.NewGuid() };
				using (var context = new SamuraiContext())
				{
					context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
					context.Samurais.Add(sam1);
					context.SaveChanges();
				}
			}

			InsertSamuraisInOneTripToDatabase();
			void InsertSamuraisInOneTripToDatabase()
			{
				var samurais = new List<SamuraiDomain>
				{
					new SamuraiDomain { Name = "Bulk Sam "  + Guid.NewGuid() },
					new SamuraiDomain { Name = "Bulk Sam 2"  + Guid.NewGuid() }
				};
				using (var context = new SamuraiContext())
				{
					context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
					context.Samurais.AddRange(samurais);
					context.SaveChanges();
				}
			}
		}

		private static void Select()
		{
			using (var context = new SamuraiContext())
			{
				var samurais = context.Samurais.ToList();
			}
		}

	}
}
