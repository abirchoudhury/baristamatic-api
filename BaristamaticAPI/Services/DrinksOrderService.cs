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
			//request good, get the recipe for drink in order
			var recipe = DrinkRecipe.GetRecipe(order.DrinkName);
			decimal totalCost = 0.00M;

			//recipe found, loop thru ingredients
			if (recipe != null && recipe.RecipeIngredients != null)
			{
				//if no ingredients then restore them. This should only happen once during app lifecycle
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
					DrinkName = DrinkRecipe.GetDrinkName(order.DrinkName),					
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

		

		public bool ValidateRequest(OrderRequestModel order)
		{
			if (order.Id > 0 || order.DrinkName > 0)
			{
				return true;
			}
			return false;
		}

		
	}
}
