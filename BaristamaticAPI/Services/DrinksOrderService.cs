using BaristamaticAPI.Models;

using Microsoft.EntityFrameworkCore;

using System.Text.RegularExpressions;

namespace BaristamaticAPI.Services
{
	public class DrinksOrderService : IDrinksOrderService
	{
		private readonly BaristamaticContext _context;
		public DrinksOrderService(BaristamaticContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Place an order, update ingredient quantities used, save changes to DB.
		/// </summary>
		/// <param name="order"></param>
		/// <returns>True for a successful order, otherwise False </returns>
		/// <exception cref="InvalidOperationException">Thrown when not enough quantity available for order</exception>	
		public async Task<bool> PlaceOrder(OrderRequestModel order)
		{
			//validate request
			if (!ValidateRequest(order))
			{
				return false;
			}

			//order placed, get the recipe for drink in order
			DrinkRecipe recipe = GetRecipe(order.DrinkName);
			double totalCost = 0;

			//recipe found, loop thru ingredients
			if (recipe != null && recipe.Ingredients != null)
			{
				//if no ingredients then restore them. This should only happen the first time during app lifecycle
				if (!_context.Ingredients.Any())
				{
					_context.RestoreIngredients();
				}
				foreach (var recIng in recipe.Ingredients)
				{
					//find matching ingredient by name
					var dbIng = _context.Ingredients.FirstOrDefault(a => a.IngredientName == recIng.IngredientName);
					if (dbIng != null)
					{
						//match found, check if ingredient in stock
						if (dbIng.Quantity <= 0 || dbIng.Quantity < recIng.Quantity)
						{
							throw new InvalidOperationException($"Ingredient out of stock. Ingredient:{dbIng.IngredientName}");
						}

						//in stock, subtract the quantity used
						dbIng.Quantity -= recIng.Quantity;

						//update ingredients in db
						_context.Ingredients.Update(dbIng);

						//ingredients updated, determine cost
						totalCost += dbIng.UnitCost * recIng.Quantity;
					}
				}
				//create the order
				var response = new OrderResponseModel
				{
					Id = order.Id,
					DrinkName = GetDrinkName(order.DrinkName),
					Ingredients = recipe.Ingredients,
					Decaf = order.Decaf,
					OrderTotal = totalCost
				};

				_context.DrinksOrder.Add(response);
				await _context.SaveChangesAsync();

				return true;
			}
			return false;
		}

		public string GetDrinkName(DrinkNames id)
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

		public bool ValidateRequest(OrderRequestModel order)
		{
			if (order.Id > 0 || order.DrinkName > 0)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Determines the ingredients required for a drink. For demo purposes, this is being hard coded.		
		/// </summary>
		/// <param name="drinkID"></param>
		/// <returns>The name and list of ingredients for the specified drink</returns>
		public DrinkRecipe GetRecipe(DrinkNames drinkName)
		{
			var result = new DrinkRecipe
			{
				Ingredients = new List<Ingredient>()
			};
			switch (drinkName)
			{
				case DrinkNames.Coffee:
					result.DrinkName = "Coffee";
					result.Ingredients?.AddRange(new List<Ingredient>
					{
						new Ingredient
						{
							IngredientName = "Coffee",
							Quantity = 3
						},
						new Ingredient
						{
							IngredientName = "Sugar",
							Quantity = 1
						},
						new Ingredient
						{
							IngredientName = "Cream",
							Quantity = 1
						}
					});
					break;
				case DrinkNames.DecafCoffee:
					result.DrinkName = "Decaf Coffee";
					result.Ingredients?.AddRange(new List<Ingredient>
					{
						new Ingredient
						{
							IngredientName = "Decaf Coffee",
							Quantity = 3
						},
						new Ingredient
						{
							IngredientName = "Sugar",
							Quantity = 1
						},
						new Ingredient
						{
							IngredientName = "Cream",
							Quantity = 1
						}
					});
					break;
				case DrinkNames.CaffeLatte:
					result.DrinkName = "Caffee Latte";
					result.Ingredients?.AddRange(new List<Ingredient>
					{
						new Ingredient
						{
							IngredientName = "Espresso",
							Quantity = 2
						},
						new Ingredient
						{
							IngredientName = "Steamed Milk",
							Quantity = 1
						}
					});
					break;
				case DrinkNames.CaffeAmericano:
					result.DrinkName = "Caffe Americano";
					result.Ingredients?.AddRange(new List<Ingredient>
					{
						new Ingredient
						{
							IngredientName = "Espresso",
							Quantity = 3
						}
					});
					break;
				case DrinkNames.CaffeMocha:
					result.DrinkName = "Caffe Mocha";
					result.Ingredients?.AddRange(new List<Ingredient>
					{
						new Ingredient
						{
							IngredientName = "Espresso",
							Quantity = 1
						},
						new Ingredient
						{
							IngredientName = "Cocoa",
							Quantity = 1
						},
						new Ingredient
						{
							IngredientName = "Steamed Milk",
							Quantity = 1
						},
						new Ingredient
						{
							IngredientName = "Whipped Cream",
							Quantity = 1
						}
					});
					break;
				case DrinkNames.Cappuccino:
					result.DrinkName = "Cappuccino";
					result.Ingredients?.AddRange(new List<Ingredient>
					{
						new Ingredient
						{
							IngredientName = "Espresso",
							Quantity = 2
						},
						new Ingredient
						{
							IngredientName = "Steamed Milk",
							Quantity = 1
						},
						new Ingredient
						{
							IngredientName = "Foamed Milk",
							Quantity = 1
						}
					});
					break;				
			}
			return result;
		}
	}
}
