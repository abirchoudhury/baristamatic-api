namespace BaristamaticAPI.Models
{
	public class DrinkRecipe
	{
		public string? DrinkName { get; set; }
		public List<RecipeIngredient> RecipeIngredients { get; set;}
		/// <summary>
		/// Gets the string client representation of the drink
		/// <see cref="DrinkNames"/>
		/// </summary>
		/// <param name="id"></param>
		/// <returns>string</returns>
		public static string GetDrinkName(DrinkNames id)
		{
			string result = "";
			switch (id)
			{
				case DrinkNames.Coffee:
					result = "Coffee";
					break;
				case DrinkNames.DecafCoffee:
					result = "Decaf Coffee";
					break;
				case DrinkNames.CaffeLatte:
					result = "Caffe Latte";
					break;
				case DrinkNames.CaffeAmericano:
					result = "Caffe Americano";
					break;
				case DrinkNames.CaffeMocha:
					result = "Caffe Mocha";
					break;
				case DrinkNames.Cappuccino:
					result = "Cappuccino";
					break;
				default:
					break;
			}
			return result;
		}

		/// <summary>
		/// Determines the ingredients required for a drink. For demo purposes, this is being hard coded.		
		/// </summary>
		/// <param name="drinkID"></param>
		/// <returns>The name and list of ingredients for the specified drink</returns>
		public static DrinkRecipe GetRecipe(DrinkNames drinkName)
		{
			var result = new DrinkRecipe
			{
				RecipeIngredients = new List<RecipeIngredient>()
			};
			switch (drinkName)
			{
				case DrinkNames.Coffee:
					result.DrinkName = "Coffee";
					result.RecipeIngredients?.AddRange(new List<RecipeIngredient>
					{
						new RecipeIngredient
						{
							IngredientName = "Coffee",
							RequiredQuantity = 3
						},
						new RecipeIngredient
						{
							IngredientName = "Sugar",
							RequiredQuantity = 1
						},
						new RecipeIngredient
						{
							IngredientName = "Cream",
							RequiredQuantity = 1
						}
					});
					break;
				case DrinkNames.DecafCoffee:
					result.DrinkName = "Decaf Coffee";
					result.RecipeIngredients?.AddRange(new List<RecipeIngredient>
					{
						new RecipeIngredient
						{
							IngredientName = "Decaf Coffee",
							RequiredQuantity = 3
						},
						new RecipeIngredient
						{
							IngredientName = "Sugar",
							RequiredQuantity = 1
						},
						new RecipeIngredient
						{
							IngredientName = "Cream",
							RequiredQuantity = 1
						}
					});
					break;
				case DrinkNames.CaffeLatte:
					result.DrinkName = "Caffee Latte";
					result.RecipeIngredients?.AddRange(new List<RecipeIngredient>
					{
						new RecipeIngredient
						{
							IngredientName = "Espresso",
							RequiredQuantity = 2
						},
						new RecipeIngredient
						{
							IngredientName = "Steamed Milk",
							RequiredQuantity = 1
						}
					});
					break;
				case DrinkNames.CaffeAmericano:
					result.DrinkName = "Caffe Americano";
					result.RecipeIngredients?.AddRange(new List<RecipeIngredient>
					{
						new RecipeIngredient
						{
							IngredientName = "Espresso",
							RequiredQuantity = 3
						}
					});
					break;
				case DrinkNames.CaffeMocha:
					result.DrinkName = "Caffe Mocha";
					result.RecipeIngredients?.AddRange(new List<RecipeIngredient>
					{
						new RecipeIngredient
						{
							IngredientName = "Espresso",
							RequiredQuantity = 1
						},
						new RecipeIngredient
						{
							IngredientName = "Cocoa",
							RequiredQuantity = 1
						},
						new RecipeIngredient
						{
							IngredientName = "Steamed Milk",
							RequiredQuantity = 1
						},
						new RecipeIngredient
						{
							IngredientName = "Whipped Cream",
							RequiredQuantity = 1
						}
					});
					break;
				case DrinkNames.Cappuccino:
					result.DrinkName = "Cappuccino";
					result.RecipeIngredients?.AddRange(new List<RecipeIngredient>
					{
						new RecipeIngredient
						{
							IngredientName = "Espresso",
							RequiredQuantity = 2
						},
						new RecipeIngredient
						{
							IngredientName = "Steamed Milk",
							RequiredQuantity = 1
						},
						new RecipeIngredient
						{
							IngredientName = "Foamed Milk",
							RequiredQuantity = 1
						}
					});
					break;
			}
			return result;
		}
	}

	public class RecipeIngredient
	{		
		public string? IngredientName { get; set; }
		public int RequiredQuantity { get; set; }
	}
}
