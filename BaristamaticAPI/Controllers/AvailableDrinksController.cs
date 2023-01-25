using BaristamaticAPI.Models;
using BaristamaticAPI.Services;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaristamaticAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AvailableDrinksController : ControllerBase
	{
		private readonly BaristamaticContext _context;
		private readonly IDrinksMenuService _drinksMenuService; 
		public AvailableDrinksController(BaristamaticContext context)
		{
			_context = context;
			_drinksMenuService = new DrinksMenuService(context);
		}

		// GET: api/<AvailableDrinksController>
		[HttpGet]
		public string Get()
		{
			var result = new List<AvailableDrinkResponseModel>();
			
			List<AvailableDrink> availDrinks = _drinksMenuService.GetAvailableDrinks();

			foreach (var drink in availDrinks)
			{
				result.Add(new AvailableDrinkResponseModel
				{
					DrinkName = drink.DrinkName,
					IsAvailable = drink.IsAvailable.GetValueOrDefault(),
					DrinkDetails = drink
				});
				drink.DrinkName = null; //remove duplicate occurance;			
			}

			var formatted = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore
			});
			return new string(formatted);
		}

		
	}
}
