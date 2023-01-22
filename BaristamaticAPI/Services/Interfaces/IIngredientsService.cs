using BaristamaticAPI.Models;

namespace BaristamaticAPI.Services
{
	public interface IIngredientsService
	{
		public void RestockIngredients();

		public Task<List<Ingredient>> GetCurrentStock();
	}
}