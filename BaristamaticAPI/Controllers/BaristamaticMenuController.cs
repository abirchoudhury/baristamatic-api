using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaristamaticAPI.Models;

namespace BaristamaticAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BaristamaticMenuController : ControllerBase
	{
		private readonly BaristamaticContext _context;

		public BaristamaticMenuController(BaristamaticContext context)
		{
			_context = context;
			_context.PopulateMenuDefaults();
		}

		// GET: api/Menu
		[HttpGet]
		[Route("GetDrinksMenu")]
		public async Task<ActionResult<IEnumerable<BaristamaticDrink>>> GetDrinksMenu()
		{
			if (_context.DrinksMenu == null)
			{
				return NotFound();
			}
			return await _context.DrinksMenu.ToListAsync();
		}

		// GET: api/Menu/5
		[HttpGet("{id}")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<ActionResult<BaristamaticDrink>> GetBaristamaticDrink(int id)
		{
			if (_context.DrinksMenu == null)
			{
				return NotFound();
			}
			var baristamaticDrink = await _context.DrinksMenu.FindAsync(id);

			if (baristamaticDrink == null)
			{
				return NotFound();
			}

			return baristamaticDrink;
		}

		// PUT: api/Menu/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<IActionResult> PutBaristamaticDrink(int id, BaristamaticDrink baristamaticDrink)
		{
			if (id != baristamaticDrink.ID)
			{
				return BadRequest();
			}

			_context.Entry(baristamaticDrink).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!BaristamaticDrinkExists(id))
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

		// POST: api/Menu
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<ActionResult<BaristamaticDrink>> PostBaristamaticDrink(BaristamaticDrink baristamaticDrink)
		{
			if (_context.DrinksMenu == null)
			{
				return Problem("Entity set 'BaristamaticContext.DrinksMenu'  is null.");
			}
			_context.DrinksMenu.Add(baristamaticDrink);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetBaristamaticDrink", new { id = baristamaticDrink.ID }, baristamaticDrink);
		}

		// DELETE: api/Menu/5
		[HttpDelete("{id}")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<IActionResult> DeleteBaristamaticDrink(int id)
		{
			if (_context.DrinksMenu == null)
			{
				return NotFound();
			}
			var baristamaticDrink = await _context.DrinksMenu.FindAsync(id);
			if (baristamaticDrink == null)
			{
				return NotFound();
			}

			_context.DrinksMenu.Remove(baristamaticDrink);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool BaristamaticDrinkExists(int id)
		{
			return (_context.DrinksMenu?.Any(e => e.ID == id)).GetValueOrDefault();
		}
	}
}
