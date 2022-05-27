using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
  public class FavoritesService
  {

    private readonly FavoritesRepository _repo;

    public FavoritesService(FavoritesRepository repo)
    {
      _repo = repo;
    }

    // GET by Account
    internal List<FavoriteModel> GetFavoritedRecipes(string id)
    {
      return _repo.GetFavoritedRecipes(id);
    }
    // GET BY ID
    internal Recipe Get(int id)
    {
      Recipe recipe = _repo.Get(id);
      if (recipe == null)
      {
        throw new Exception("Invalid Id");
      }
      return recipe;
    }
    // CREATE
    internal Favorite Create(Favorite favoriteData)
    {
      return _repo.Create(favoriteData);
    }
    // DELETE
    internal void Delete(int id, string userId)
    {
      Recipe recipe = Get(id);
      if (recipe.CreatorId != userId)
      {
        throw new Exception("You cannot delete this!");
      }
      _repo.Delete(id);
    }
  }

}