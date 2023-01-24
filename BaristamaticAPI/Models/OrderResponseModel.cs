using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaristamaticAPI.Models
{
	public class OrderResponseModel
	{
		[Key]
		[JsonIgnore]
		public int Id { get; set; }
		public string? DrinkName { get; set; }
		public bool Decaf { get; set; }		
		public decimal OrderTotal { get; set; }
		public string? OrderDateFormatted { get; set; }
		public string? TotalCostFormatted { get; set; }
		[JsonIgnore]
		public DateTime OrderDate { get; set; }

		public DateTime UTCOrderDate { get; set; }
	}
}
