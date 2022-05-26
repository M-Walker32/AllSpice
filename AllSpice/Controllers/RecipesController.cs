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

    public RecipesController(RecipesService rs)
    {
      _rs = rs;
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
    // CREATE
    [Authorize]
    [HttpPost]
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
    // DELETE
  }
}