﻿using BaristamaticAPI.Models;

namespace BaristamaticAPI.Services
{
	public class DrinksMenuService : IDrinksMenuService
	{
		private readonly BaristamaticContext _context;
		private readonly IIngredientsService _ingredientsService;
		public DrinksMenuService(BaristamaticContext context)
		{
			_context = context;
			_ingredientsService = new IngredientsService(context);
		}
		public List<AvailableDrink> GetAvailableDrinks()
		{
			var result = new List<AvailableDrink>
			{
				GetAvailableDrink(DrinkNames.Coffee),
				GetAvailableDrink(DrinkNames.DecafCoffee),
				GetAvailableDrink(DrinkNames.CaffeLatte),
				GetAvailableDrink(DrinkNames.CaffeMocha),
				GetAvailableDrink(DrinkNames.CaffeAmericano),
				GetAvailableDrink(DrinkNames.Cappuccino)
			};
			return result;
		}

		private AvailableDrink GetAvailableDrink(DrinkNames drinkName)
		{
			if (!_context.Ingredients.Any())
			{
				_context.RestoreIngredients();
			}
			var result = new AvailableDrink();
			var recipe = DrinkRecipe.GetRecipe(drinkName);
			result.DrinkName = recipe.DrinkName;
			result.RecipeIngredients = recipe.RecipeIngredients;
			result.AvailableIngredients = new List<RecipeIngredient>();

			foreach (var ing in result.RecipeIngredients)
			{
				var currIng = _context.Ingredients.FirstOrDefault(a => a.IngredientName == ing.IngredientName);
				if (currIng != null)
				{
					result.AvailableIngredients.Add(
						new RecipeIngredient
						{							
							IngredientName = currIng.IngredientName,
							RequiredQuantity = currIng.Quantity
						});

					result.IsAvailable = ing.RequiredQuantity <= currIng.Quantity;
				}

			}

			return result;			

		}
	}
}

