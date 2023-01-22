using BaristamaticAPI.Models;

namespace BaristamaticAPI.Services
{
	public interface IDrinksOrderService
	{
		public Task<bool> PlaceOrder(OrderRequestModel order);
	}
}