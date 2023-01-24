namespace BaristamaticAPI.Models
{
	public class DrinkRecipe
	{
		public string? DrinkName { get; set; }
		public List<RecipeIngredient>? RecipeIngredients { get; set;}		
	}

	public class RecipeIngredient
	{
		public int IngredientID { get; set; }
		public string? IngredientName { get; set; }
		public int RequiredQuantity { get; set; }
	}
}
