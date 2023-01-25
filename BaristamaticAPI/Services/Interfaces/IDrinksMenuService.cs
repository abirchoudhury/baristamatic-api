using BaristamaticAPI.Models;

namespace BaristamaticAPI.Services
{
	public interface IDrinksMenuService
	{
		List<AvailableDrink> GetAvailableDrinks();
	}
}