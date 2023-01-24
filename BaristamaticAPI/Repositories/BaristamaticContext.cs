using Microsoft.EntityFrameworkCore;

namespace BaristamaticAPI.Models
{
	public class BaristamaticContext : DbContext
	{
		public BaristamaticContext(DbContextOptions<BaristamaticContext> options) : base(options)
		{			
		}
		public DbSet<BaristamaticDrink> DrinksMenu { get; set; }
		public DbSet<OrderResponseModel> DrinksOrder { get; set; }
		public DbSet<Ingredient> Ingredients { get; set; }

		public void RestoreIngredients()
		{
			var ilst = new List<Ingredient>
			{
				new Ingredient
				{
					IngredientID = 1,
					IngredientName = "Coffee",
					UnitCost = 0.75M,
					Quantity = 10
				},
				new Ingredient
				{
					IngredientID = 2,
					IngredientName = "Decaf Coffee",
					UnitCost = 0.75M,
					Quantity = 10
				},
				new Ingredient
				{
					IngredientID = 3,
					IngredientName = "Sugar",
					UnitCost = 0.25M,
					Quantity = 10
				},
				new Ingredient
				{
					IngredientID = 4,
					IngredientName = "Cream",
					UnitCost = 0.25M,
					Quantity = 10
				},
				new Ingredient
				{
					IngredientID = 5,
					IngredientName = "Steamed Milk",
					UnitCost = 0.35M,
					Quantity = 10
				},
				new Ingredient
				{
					IngredientID = 6,
					IngredientName = "Foamed Milk",
					UnitCost = 0.35M,
					Quantity = 10
				},
				new Ingredient
				{
					IngredientID = 7,
					IngredientName = "Espresso",
					UnitCost = 1.10M,
					Quantity = 10
				},
				new Ingredient
				{
					IngredientID = 8,
					IngredientName = "Cocoa",
					UnitCost = 0.90M,
					Quantity = 10
				},
				new Ingredient
				{
					IngredientID = 9,
					IngredientName = "Whipped Cream",
					UnitCost = 1.00M,
					Quantity = 10
				}
			};

			if (!this.Ingredients.Any())
			{
				this.Ingredients.AddRange(ilst);
			}
			
			
			this.SaveChanges();
		}

		public void PopulateMenuDefaults()
		{
			var dlst = new List<BaristamaticDrink>
			{
				new BaristamaticDrink
				{
					ID = 1,
					DrinkName = "Coffee",
					Ingredients = "3 units of coffee, 1 unit of sugar, 1 unit of cream"
				},

				new BaristamaticDrink
				{
					ID = 2,
					DrinkName = "Decaf Coffee",
					Ingredients = "3 units of Decaf Coffee, 1 unit of sugar, 1 unit of cream"
				},

				new BaristamaticDrink
				{
					ID = 3,
					DrinkName = "Caffe Latte",
					Ingredients = "2 units of espresso, 1 unit of steamed milk"
				},

				new BaristamaticDrink
				{
					ID = 4,
					DrinkName = "Caffe Americano",
					Ingredients = "3 units of espresso"
				},

				new BaristamaticDrink
				{
					ID = 5,
					DrinkName = "Caffe Mocha",
					Ingredients = "1 units of Espresso, 1 unit of cocoa, 1 unit of steamed milk, 1 unit of whipped cream"
				},

				new BaristamaticDrink
				{
					ID = 6,
					DrinkName = "Cappuccino",
					Ingredients = "2 units of Espresso, 1 unit of steamed milk, 1 unit of foamed milk"
				}
			};
			if (!this.DrinksMenu.Any())
			{
				this.DrinksMenu.AddRange(dlst);
			}
			this.SaveChanges();
		}


	}
}
