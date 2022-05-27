using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using AllSpice.Models;
using AllSpice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AllSpice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class RecipesController : ControllerBase
  {
    private readonly RecipesService _rs;
    private readonly StepsService _ss;
    private readonly IngredientsService _ings;

    public RecipesController(RecipesService rs, StepsService ss, IngredientsService ings)
    {
      _rs = rs;
      _ss = ss;
      _ings = ings;
    }

    // GET
    [HttpGet]
    public ActionResult<List<Recipe>> Get()
    {
      try
      {
        List<Recipe> recipe = _rs.Get();
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return Ok(e.Message);
      }
    }
    // GET BY ID
    [HttpGet("{id}")]
    public ActionResult<List<Recipe>> Get(int id)
    {
      try
      {
        Recipe recipe = _rs.Get(id);
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return Ok(e.Message);
      }
    }
    // Get Steps by Recipe
    [HttpGet("{id}/steps")]
    public ActionResult<List<Step>> GetSteps(int id)
    {
      try
      {
        List<Step> steps = _ss.GetSteps(id);
        return Ok(steps);
      }
      catch (Exception e)
      {
        return Ok(e.Message);
      }
    }
    // Get ingredients by Recipe
    [HttpGet("{id}/ingredients")]
    public ActionResult<List<Ingredient>> GetIngredients(int id)
    {
      try
      {
        List<Ingredient> ingredients = _ings.GetIngredients(id);
        return Ok(ingredients);
      }
      catch (Exception e)
      {
        return Ok(e.Message);
      }
    }
    // CREATE
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Recipe>> Create([FromBody] Recipe recipeData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        recipeData.CreatorId = userInfo.Id;
        Recipe recipe = _rs.Create(recipeData);
        recipe.Creator = userInfo;
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // EDIT
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Recipe>> Edit(int id, [FromBody] Recipe recipeData)
    {
      try
      {
        Account userinfo = await HttpContext.GetUserInfoAsync<Account>();
        recipeData.CreatorId = userinfo.Id;
        recipeData.Id = id;
        Recipe recipe = _rs.Edit(recipeData);
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // DELETE
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Recipe>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _rs.Delete(id, userInfo.Id);
        return Ok("delorted");

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}