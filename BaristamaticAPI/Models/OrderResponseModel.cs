namespace BaristamaticAPI.Models
{
	public class OrderResponseModel
	{
		public int Id { get; set; }
		public string? DrinkName { get; set; }
		public bool? Decaf { get; set; }
		public List<Ingredient>? Ingredients { get; set; }
		public double OrderTotal { get; set; }

		public DateTime OrderDate { get; set; }
	}
}
