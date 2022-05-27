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
  public class IngredientsController : ControllerBase
  {
    private readonly IngredientsService _ings;
    public IngredientsController(IngredientsService ings)
    {
      _ings = ings;
    }
    // // GET
    // [HttpGet]
    // public ActionResult<List<Step>> Get()
    // {
    //   try
    //   {
    //     List<Step> step = _ss.Get();
    //     return Ok(step);
    //   }
    //   catch (Exception e)
    //   {
    //     return Ok(e.Message);
    //   }
    // }
    // GET BY ID
    [HttpGet("{id}")]
    public ActionResult<List<Ingredient>> Get(int id)
    {
      try
      {
        Ingredient ingredient = _ings.Get(id);
        return Ok(ingredient);
      }
      catch (Exception e)
      {
        return Ok(e.Message);
      }
    }
    // CREATE
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Ingredient>> Create([FromBody] Ingredient ingredientData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        ingredientData.CreatorId = userInfo.Id;
        Ingredient ingredient = _ings.Create(ingredientData);
        ingredient.Creator = userInfo;
        return Ok(ingredient);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // Delete
    [HttpDelete("{id}")]
    [Authorize]

    public async Task<ActionResult<Ingredient>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _ings.Delete(id, userInfo.Id);
        return Ok("delorted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}