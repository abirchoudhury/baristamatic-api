using BaristamaticAPI.Models;

namespace BaristamaticAPI.Services
{
	public interface IDrinksOrderService
	{
		public Task<OrderResponseModel> PlaceOrder(OrderRequestModel order);
	}
}