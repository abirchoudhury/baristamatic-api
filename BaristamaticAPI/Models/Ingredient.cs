using System.ComponentModel.DataAnnotations;

namespace BaristamaticAPI.Models
{
	public class Ingredient
	{
		[Key]
		public int IngredientID { get; set; }
		public string? IngredientName { get; set; }
		public decimal UnitCost { get; set; }
		public int Quantity { get; set; }
	}
}
