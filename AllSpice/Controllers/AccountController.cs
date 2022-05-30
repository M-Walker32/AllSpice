using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AllSpice.Models;
using AllSpice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly FavoritesService _fs;
    private readonly RecipesService _rs;

    public AccountController(AccountService accountService, FavoritesService fs, RecipesService rs)
    {
      _accountService = accountService;
      _fs = fs;
      _rs = rs;
    }
    // Get Account
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // Get Users Favorites
    [HttpGet("Favorites")]
    public async Task<ActionResult<List<FavoriteModel>>> GetFavoritedRecipes()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<FavoriteModel> recipes = _fs.GetFavoritedRecipes(userInfo.Id);
        return Ok(recipes);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }


}