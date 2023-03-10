using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BaristamaticAPI.Models
{
	public class OrderRequestModel
	{
		[Key]
		[JsonIgnore]
		public int Id { get; set; }
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public DrinkNames DrinkName { get; set; }
		[DefaultValue(false)]
		public bool Decaf { get; set; }
	}

	public enum DrinkNames
	{
		Coffee = 1,
		DecafCoffee = 2,
		CaffeLatte = 3,
		CaffeAmericano = 4,
		CaffeMocha = 5,
		Cappuccino = 6
	}
}
