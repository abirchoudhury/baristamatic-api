using BaristamaticAPI.Models;

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
		/// <returns>The order created</returns>
		/// <exception cref="InvalidOperationException">Thrown when not enough quantity available for order</exception>	
		public async Task<OrderResponseModel> PlaceOrder(OrderRequestModel order)
		{
			//validate request
			if (!ValidateRequest(order))
			{
				throw new ArgumentException("Invalid request model", nameof(order));
			}

			var response = new OrderResponseModel();
			//order placed, get the recipe for drink in order
			DrinkRecipe recipe = GetRecipe(order.DrinkName);
			decimal totalCost = 0.00M;

			//recipe found, loop thru ingredients
			if (recipe != null && recipe.RecipeIngredients != null)
			{
				//if no ingredients then restore them. This should only happen the first time during app lifecycle
				if (!_context.Ingredients.Any())
				{
					_context.RestoreIngredients();
				}
				foreach (var recIng in recipe.RecipeIngredients)
				{
					//find matching ingredient by name
					var dbIng = _context.Ingredients.FirstOrDefault(a => a.IngredientName == recIng.IngredientName);
					if (dbIng != null)
					{
						//match found, check if ingredient in stock
						if (dbIng.Quantity <= 0 || dbIng.Quantity < recIng.RequiredQuantity)
						{
							throw new InvalidOperationException($"Ingredient out of stock. Ingredient:{dbIng.IngredientName}");
						}

						//in stock, subtract the quantity used
						dbIng.Quantity -= recIng.RequiredQuantity;

						//update ingredients in db						
						_context.Ingredients.Update(dbIng);

						//ingredients updated, determine cost
						totalCost += dbIng.UnitCost * recIng.RequiredQuantity;
					}
				}
				//create the order
				response = new OrderResponseModel
				{
					Id = order.Id,
					DrinkName = GetDrinkName(order.DrinkName),					
					Decaf = order.Decaf,
					OrderTotal = totalCost,
					OrderDate = DateTime.Now,
					UTCOrderDate = DateTime.UtcNow					
				};

				_context.DrinksOrder.Add(response);
				await _context.SaveChangesAsync();				
			}
			return response;
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
}
