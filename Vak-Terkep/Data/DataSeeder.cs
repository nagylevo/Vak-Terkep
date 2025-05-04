using Vak_Terkep.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;


namespace Vak_Terkep.Data
{
	public class DataSeeder
	{
		private readonly VakTerkepDbContext _context;

		public DataSeeder(VakTerkepDbContext context)
		{
			_context = context;
		}

		public void SeedData()
		{
            _context.Database.EnsureCreated(); //Ezt a bemutatás egyszerűsítéséért tettük bele, eredetileg nem használtuk.

            if (_context.Routes.Any()) return; 

		
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "teremLeirasok.txt");
			var lines = File.ReadAllLines(filePath);


			foreach (var line in lines)
			{
				var columns = line.Split('|');
				if (columns.Length == 4)
				{
					var route = new Route
					{
						textName = columns[0].Trim(),
						buildingName = columns[1].Trim(),
						floorName = columns[2].Trim(),
						description = columns[3].Trim()
					};

					_context.Routes.Add(route);
				}
			}

			_context.SaveChanges();
		}
	}
}
