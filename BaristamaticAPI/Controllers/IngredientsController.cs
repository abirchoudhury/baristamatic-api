using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaristamaticAPI.Models;
using System.Runtime.CompilerServices;
using BaristamaticAPI.Services;

namespace BaristamaticAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IngredientsController : ControllerBase
	{
		private readonly BaristamaticContext _context;
		private readonly IIngredientsService _ingredientsService;
		public IngredientsController(BaristamaticContext context)
		{
			_context = context;
			_ingredientsService = new IngredientsService(context);
			if (!_context.Ingredients.Any())
			{
				_context.RestoreIngredients();
			}			
		}

		// GET: api/Ingredients
		[HttpGet]
		[Route("GetCurrentStock")]
		public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
		{
			if (_context.Ingredients == null)
			{
				return Problem("There was a problem with the database");
			}
			var response = await _context.Ingredients.ToListAsync();
			if (response == null)
			{
				return Problem("There was a problem with the database, no ingredients foundl.");
			}
			else
			{
				return response;
			}
		}

		[HttpPost]
		[Route("RestoreIngredients")]
		public JsonResult RestockIngredients()
		{
			_ingredientsService.RestockIngredients();
			return new JsonResult(Ok("Ingredients restocked successfully"));
		}

		#region IgnoredApis
		// GET: api/Ingredients/5
		[HttpGet("{id}")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<ActionResult<Ingredient>> GetIngredient(int id)
		{
			if (_context.Ingredients == null)
			{
				return NotFound();
			}
			var ingredient = await _context.Ingredients.FindAsync(id);

			if (ingredient == null)
			{
				return NotFound();
			}

			return ingredient;
		}

		

		// POST: api/Ingredients
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<ActionResult<Ingredient>> PostIngredient(Ingredient ingredient)
		{
			if (_context.Ingredients == null)
			{
				return Problem("Entity set 'IngredientContext.Ingredients'  is null.");
			}
			_context.Ingredients.Add(ingredient);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetIngredient", new { id = ingredient.IngredientID }, ingredient);
		}

		// DELETE: api/Ingredients/5
		[HttpDelete("{id}")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<IActionResult> DeleteIngredient(int id)
		{
			if (_context.Ingredients == null)
			{
				return NotFound();
			}
			var ingredient = await _context.Ingredients.FindAsync(id);
			if (ingredient == null)
			{
				return NotFound();
			}

			_context.Ingredients.Remove(ingredient);
			await _context.SaveChangesAsync();

			return NoContent();
		}
		#endregion

		private bool IngredientExists(int id)
		{
			return (_context.Ingredients?.Any(e => e.IngredientID == id)).GetValueOrDefault();
		}
	}
}
