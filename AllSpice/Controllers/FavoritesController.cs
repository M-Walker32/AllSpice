using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AllSpice.Models;
using AllSpice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodeWorks.Auth0Provider;

namespace AllSpice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class FavoritesController : ControllerBase
  {
    private readonly FavoritesService _fs;

    public FavoritesController(FavoritesService fs)
    {
      _fs = fs;
    }
    // Get By ID
    [HttpGet("{id}")]
    public ActionResult<List<Favorite>> Get(int id)
    {
      try
      {
        Favorite favorite = _fs.Get(id);
        return Ok(favorite);
      }
      catch (Exception e)
      {
        return Ok(e.Message);
      }
    }
    // Get Users Favorites
    // public async Task<ActionResult<List<FavoriteModel>>> GetFavoritedRecipes()
    // {
    //   try
    //   {
    //     Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
    //     List<FavoriteModel> recipe = _fs.GetFavoritedRecipes(userInfo.Id);
    //     return Ok(recipe);
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }
    // Create Favorite
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Favorite>> Create([FromBody] Favorite favoriteData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        favoriteData.AccountId = userInfo.Id;
        favoriteData.CreatorId = userInfo.Id;
        Favorite favorite = _fs.Create(favoriteData);
        return Ok(favorite);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // DELETE Favorite
    [HttpDelete("{id}")]
    [Authorize]

    public async Task<ActionResult<Favorite>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _fs.Delete(id, userInfo.Id);
        return Ok("delorted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}