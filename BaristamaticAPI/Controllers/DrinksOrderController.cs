using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaristamaticAPI.Models;
using BaristamaticAPI.Services;
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
				return NotFound();
			}
			return await _context.DrinksOrder.ToListAsync();
		}

		// GET: api/DrinksOrder/5
		[HttpGet("{id}")]
		public async Task<ActionResult<OrderResponseModel>> GetDrinksOrder(int id)
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

			return drinksOrder;
		}

		[HttpPost]
		[Route("PlaceDrinksOrder")]
		public async Task<ActionResult<OrderResponseModel>> PlaceDrinksOrder(OrderRequestModel drinksOrder)
		{
			if (_context.DrinksOrder == null)
			{
				return Problem("Entity set 'DrinksOrderContext.DrinkOrders'  is null.");
			}
			bool result = await _orderService.PlaceOrder(drinksOrder);
			if (result)
			{
				return CreatedAtAction("PlaceDrinksOrder", new { id = drinksOrder.Id }, drinksOrder);
			}
			else
			{
				return Problem("Failed to place order, unkown error occurred.");
			}
			
		}

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

			return NoContent();
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

			return NoContent();
		}

		private bool DrinksOrderExists(int id)
		{
			return (_context.DrinksOrder?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
