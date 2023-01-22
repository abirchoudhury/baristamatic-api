using BaristamaticAPI.Models;

using Microsoft.EntityFrameworkCore;

namespace BaristamaticAPI.Services
{
	public class IngredientsService : IIngredientsService
	{
		private readonly BaristamaticContext _context;
		public IngredientsService(BaristamaticContext context)
		{
			this._context = context;
		}

		/// <summary>
		/// Restores quantities for all ingredients.
		/// </summary>
		public void RestockIngredients()
		{
			//restore all quantities to 10
			_context.Ingredients.ForEachAsync(x => { x.Quantity = 10; });
			_context.SaveChangesAsync();
		}

		/// <summary>
		/// Gets the current list, useful for checking ingredients after an order is placed.
		/// </summary>
		/// <returns>A list of ingredients </returns>
		public async Task<List<Ingredient>> GetCurrentStock()
		{

			var result = await _context.Ingredients.ToListAsync();

			return result;
		}
	}
}
