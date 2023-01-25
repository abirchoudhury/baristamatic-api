

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BaristamaticAPI.Models
{
	public class AvailableDrink : DrinkRecipe
	{
		[JsonIgnore]
		public bool? IsAvailable { get; set; }
		public List<RecipeIngredient> AvailableIngredients { get; set; }
		
	}

	public class AvailableDrinkResponseModel
	{
		public string? DrinkName { get; set; }
		public bool IsAvailable { get; set; }
		public AvailableDrink DrinkDetails { get; set; }
	}
}
