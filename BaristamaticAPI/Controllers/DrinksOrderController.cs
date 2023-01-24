using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaristamaticAPI.Models;
using BaristamaticAPI.Services;
using System.Globalization;

namespace BaristamaticAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DrinksOrderController : ControllerBase
	{
		private readonly BaristamaticContext _context;
		private readonly IDrinksOrderService _orderService;

		public DrinksOrderController(BaristamaticContext context)
		{
			_context = context;
			_orderService = new DrinksOrderService(context);
		}

		// GET: api/DrinksOrder
		[HttpGet]
		[Route("GetAllOrders")]
		public async Task<ActionResult<IEnumerable<OrderResponseModel>>> GetDrinkOrders()
		{
			if (_context.DrinksOrder == null)
			{
				//return NotFound();
				return Problem("There was a problem with the database");
			}
			var response = await _context.DrinksOrder.ToListAsync();
			if (response == null)
			{
				return NoContent();
			}
			else
			{
				foreach (var order in response)
				{
					order.OrderDateFormatted = order.OrderDate.ToString("MM/dd/yyyy hh:mm tt");
					order.TotalCostFormatted = order.OrderTotal.ToString("C", CultureInfo.CurrentCulture);
				}

				return response;
			}
		}

		[HttpPost]
		[Route("PlaceDrinksOrder")]
		public async Task<ActionResult<OrderResponseModel>> PlaceDrinksOrder(OrderRequestModel drinksOrder)
		{
			OrderResponseModel response;
			if (_context.DrinksOrder == null)
			{
				return Problem("Entity set 'DrinksOrderContext.DrinkOrders'  is null.");
			}
			try
			{
				response = await _orderService.PlaceOrder(drinksOrder);
				if (response != null)
				{
					response.OrderDateFormatted = response.OrderDate.ToString("MM/dd/yyyy hh:mm tt");
					response.TotalCostFormatted = response.OrderTotal.ToString("C", CultureInfo.CurrentCulture);

					return CreatedAtAction("PlaceDrinksOrder", new { id = response.Id }, response);
				}
				else
				{
					return Problem("Failed to place order, unkown error occurred.");
				}
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}



		}

		#region IgnoredAPIs
		// PUT: api/DrinksOrder/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<IActionResult> PutDrinksOrder(int id, OrderResponseModel drinksOrder)
		{
			if (id != drinksOrder.Id)
			{
				return BadRequest();
			}

			_context.Entry(drinksOrder).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!DrinksOrderExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return Ok("Order updated successfully");
		}



		// DELETE: api/DrinksOrder/5
		[HttpDelete("{id}")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<IActionResult> DeleteDrinksOrder(int id)
		{
			if (_context.DrinksOrder == null)
			{
				return NotFound();
			}
			var drinksOrder = await _context.DrinksOrder.FindAsync(id);
			if (drinksOrder == null)
			{
				return NotFound();
			}

			_context.DrinksOrder.Remove(drinksOrder);
			await _context.SaveChangesAsync();

			return Ok($"Deleted Order with ID:{id}");
		}
		#endregion

		private bool DrinksOrderExists(int id)
		{
			return (_context.DrinksOrder?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
